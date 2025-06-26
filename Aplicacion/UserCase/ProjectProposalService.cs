using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.UserCase
{
    public class ProjectProposalService : IProjectProposalService
    {
        private readonly IProjectProposalCommand _command;
        private readonly IProjectProposalQuery _query;
        private readonly IApprovalRuleQuery _ruleQuery;
        private readonly IProjectApprovalStepCommand _stepCommand;
        private readonly IProjectApprovalStepService _stepService;
        private readonly IAreaQuery _areaQuery;
        private readonly IProjectTypeQuery _projectTypeQuery;
        private readonly IUserQuery _userQuery;

        public ProjectProposalService(
            IProjectProposalCommand command,
            IProjectProposalQuery query,
            IApprovalRuleQuery ruleQuery,
            IProjectApprovalStepCommand stepCommand,
            IProjectApprovalStepService stepService,
            IAreaQuery areaQuery,
            IProjectTypeQuery projectTypeQuery,
            IUserQuery userQuery)
        {
            _command = command;
            _query = query;
            _ruleQuery = ruleQuery;
            _stepCommand = stepCommand;
            _stepService = stepService;
            _areaQuery = areaQuery;
            _projectTypeQuery = projectTypeQuery;
            _userQuery = userQuery;
        }

        public async Task<ProjectProposalResponseDetail> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy)
        {
            if (!await _areaQuery.Exists(area))
                throw new ValidationException("Área inválida");

            if (!await _projectTypeQuery.Exists(type))
                throw new ValidationException("Tipo de proyecto inválido");

            if (!await _userQuery.Exists(createdBy))
                throw new ValidationException("Usuario inválido");

            if (_query.ExistsByTitle(title,null))
                throw new ValidationException("El proyecto ya existe");
            ProjectProposal pp = new()
            {
                Title = title,
                Description = description,
                Area = area,
                Type = type,
                EstimatedAmount = estimatedAmount,
                EstimatedDuration = estimatedDuration,
                Status = 1,
                CreateAt = DateTime.Now,
                CreatedBy = createdBy
            };
            _ = await _command.CreateProjectProposal(pp);

            List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(pp);
            await _stepCommand.CreateProjectApprovalStep(pp, rules);

            return ProjectMapper.ToDetailResponse(pp);
        }

        public async Task<List<ProjectShortResponse>> Search(string? title, int? status, int? applicant, int? approverUser)
        {
            List<ProjectProposal> proposals = await _query.GetByFilters(title, status, applicant, approverUser);
            return ProjectMapper.ToShortResponseList(proposals);
        }

        public List<ProjectProposalResponseDetail> GetDetailByUserId(int userId)
        {
            Task<List<ProjectProposal>> propuestas = _query.GetByCreatorId(userId);
            return ProjectMapper.ToDetailResponseList(propuestas.Result);
        }

        public async Task<ProjectProposalResponseDetail>? GetById(Guid id)
        {
            var proposal = await _query.GetById(id);

            if (proposal == null)
                return null;

            return ProjectMapper.ToDetailResponse(proposal);
        }

        public bool ExistingProject(string title, Guid? excludeId)
        {
            return _query.ExistsByTitle(title, excludeId);
        }

        public async Task<ProjectProposalResponseDetail?> ProcessDecision(Guid projectId, int stepId, int userId, int status, string? observation)
        {

                ProjectProposal project = await _query.GetById(projectId);
            if (project == null)
                throw new NotFoundException("Proyecto no encontrado");

            if (project.Status is not 1 and not 4)
                return null;

            ProjectApprovalStep? step = await _stepService.GetById(stepId);
            if (step == null)
                throw new ValidationException("Paso no encontrado");

            bool updated = await _stepService.UpdateProjectApprovalStep(stepId, status, userId, observation);
            if (!updated)
                throw new ConflictException("El paso no pudo actualizarse");

            ProjectProposal updatedProject = await _query.GetById(project.Id);
            return ProjectMapper.ToDetailResponse(updatedProject);
        }

        public async Task<ProjectProposalResponseDetail?> UpdateProject(Guid id, string title, string description, int duration)
        {
            ProjectProposal proposal = await _query.GetById(id);
            if (proposal == null)
            {
                throw new NotFoundException("Proyecto no encontrado.");
            }

            if (ExistingProject(title, id))
            {
                throw new ConflictException("Ya existe un proyecto creado con ese nombre.");
            }

            if (proposal.Status != 4)
            {
                throw new ConflictException("Solo se puede editar un proyecto con estado observado.");
            }

            proposal.Title = title;
            proposal.Description = description;
            proposal.EstimatedDuration = duration;
            proposal.Status = 1;

            await _stepCommand.DeleteStepsByProposal(proposal.Id);
            List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(proposal);
            await _stepCommand.CreateProjectApprovalStep(proposal, rules);

            await _command.UpdateProjectProposal(proposal);
            return ProjectMapper.ToDetailResponse(proposal);
        }
    }
}