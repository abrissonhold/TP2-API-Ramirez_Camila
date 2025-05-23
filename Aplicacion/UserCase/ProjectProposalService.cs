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

        public ProjectProposalService(
            IProjectProposalCommand command, 
            IProjectProposalQuery query, 
            IApprovalRuleQuery ruleQuery, 
            IProjectApprovalStepCommand stepCommand,
            IProjectApprovalStepService stepService)
        {
            _command = command;
            _query = query;
            _ruleQuery = ruleQuery;
            _stepCommand = stepCommand;
            _stepService = stepService;
        }

        public async Task<ProjectProposalResponseDetail> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy)
        {
            ProjectProposal pp = new ProjectProposal
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

            ProjectProposal projectProposal = await _command.CreateProjectProposal(pp);

            List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(pp);
            await _stepCommand.CreateProjectApprovalStep(pp, rules);

            return ProjectMapper.ToDetailResponse(pp);
        }

        public async Task<List<ProjectShortResponse>> Search(string? title, int? status, int? applicant, int? approverUser)
        {
            var proposals = await _query.GetByFilters(title, status, applicant, approverUser);
            return ProjectMapper.ToShortResponseList(proposals);
        }        
        
        public List<ProjectProposalResponseDetail> GetDetailByUserId(int userId)
        {
            var propuestas = _query.GetByCreatorId(userId);
            return ProjectMapper.ToDetailResponseList(propuestas.Result);
        }

        public async Task<ProjectProposalResponseDetail> GetById(Guid id)
        {
            var proposal = await _query.GetById(id);
            return ProjectMapper.ToDetailResponse(proposal);
        }

        public bool ExistingProject(string title)
        {
            return _query.ExistsByTitle(title);
        }

        public async Task<ProjectProposalResponseDetail> ProcessDecision(Guid projectId, int stepId, int userId, int status, string? observation)
        {
            var updated = await _stepService.UpdateProjectApprovalStep(stepId, status, userId, observation);
            if (!updated) return null;

            var updatedProject = await _query.GetById(projectId);
            if (updatedProject == null) return null;

            return ProjectMapper.ToDetailResponse(updatedProject);
        }
        public async Task<ProjectProposalResponseDetail?> UpdateProject(Guid id, string title, string description, int duration)
        {
            var proposal = await _query.GetById(id);
            if (proposal == null) return null;

            if (proposal.Status != 1) return ProjectProposalResponseDetail.Conflict;

            proposal.Title = title;
            proposal.Description = description;
            proposal.EstimatedDuration = duration;

            await _command.UpdateProjectProposal(proposal);
            return ProjectMapper.ToDetailResponse(proposal);
        }
    }
}