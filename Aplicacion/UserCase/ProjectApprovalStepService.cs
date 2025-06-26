using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;

namespace Application.UserCase
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IProjectApprovalStepQuery _query;
        private readonly IProjectApprovalStepCommand _command;
        private readonly IProjectProposalCommand _projectCommand;
        private readonly IUserService _userService;
        public ProjectApprovalStepService(IProjectApprovalStepQuery query, IProjectApprovalStepCommand command, IProjectProposalCommand projectProposalCommand, IUserService userService)
        {
            _query = query;
            _command = command;
            _projectCommand = projectProposalCommand;
            _userService = userService;
        }

        public async Task<ProjectApprovalStep?> GetById(long stepId)
        {
            return await _query.GetById(stepId);
        }
        public async Task<bool> UpdateProjectApprovalStep(long stepId, int newStatus, int userId, string? obs)
        {
            ProjectApprovalStep? step = await GetById(stepId);            
            if (step == null)
                throw new ValidationException("Paso no encontrado");       
            
            if (newStatus < 1 || newStatus > 4)
                throw new ValidationException("Estado inválido.");

            if (step.Status != 1)
                throw new ConflictException("No se puede modificar un paso que ya fue decidido");

            UserResponse? user = await _userService.GetById(userId);
            if (user == null)
                throw new ValidationException("Usuario no encontrado");

            if (user.Role.Id != step.ApproverRoleId)
                throw new ValidationException("No tiene permisos para aprobar este paso");

            ProjectProposal project = step.ProjectProposal;
            ICollection<ProjectApprovalStep> allSteps = step.ProjectProposal.ProjectApprovalSteps;

            bool hayPasosPreviosSinResolver = allSteps
                .Where(s => s.StepOrder < step.StepOrder)
                .Any(s => s.Status == 1);

            if (hayPasosPreviosSinResolver)
                throw new ValidationException("Debe esperar a que se decidan los pasos anteriores");

            step.Status = newStatus;
            step.DecisionDate = DateTime.Now;
            step.ApproverUserId = userId;
            step.Observations = obs;

            bool actualizado = await _command.UpdateStep(step);
            if (!actualizado)
                return false;

            var stepInCollection = allSteps.FirstOrDefault(s => s.Id == step.Id);
            if (stepInCollection != null)
                stepInCollection.Status = newStatus;

            if (newStatus == 3)
                project.Status = 3;

            if (newStatus == 4)
                project.Status = 4;

            if (allSteps.All(s => s.Status == 2))
                project.Status = 2;

            await _projectCommand.UpdateProjectProposalStatus(project);

            return true;
        }
        public List<ProjectApprovalStep> GetPendingStepsByRole(int approverRoleId)
        {
            List<ProjectApprovalStep> allSteps = _query.GetPendingStepsByRole(approverRoleId);

            List<ProjectApprovalStep> valid = [];

            foreach (ProjectApprovalStep step in allSteps)
            {
                IEnumerable<ProjectApprovalStep> previousSteps = step.ProjectProposal.ProjectApprovalSteps
                    .Where(p => p.StepOrder < step.StepOrder);

                if (!previousSteps.Any() || previousSteps.All(p => p.Status == 2))
                {
                    valid.Add(step);
                }
            }

            return valid;
        }
    }
}
