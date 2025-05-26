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
            if (step.Status is not 1 and not 4)
            {
                throw new Conflict("Este paso ya fue decidido y no puede modificarse.");
            }

            UserResponse? user = await _userService.GetById(userId);
            if (user == null || step == null)
            {
                throw new Conflict("Datos inválidos.");
            }

            if (user.Role != step.ApproverRoleId)
            {
                throw new Conflict("El usuario elegido no puede decidir por este paso.");
            }

            step.Status = newStatus;
            step.DecisionDate = DateTime.Now;
            step.ApproverUserId = userId;
            step.Observations = obs;

            bool actualizado = await _command.UpdateStep(step);
            if (!actualizado)
            {
                return false;
            }

            ProjectProposal project = step.ProjectProposal;
            ICollection<ProjectApprovalStep> allSteps = project.ProjectApprovalSteps;

            if (newStatus == 3)
            {
                project.Status = 3;
            }

            if (newStatus == 4)
            {
                project.Status = 4;
            }

            if (allSteps.All(s => s.Status == 2))
            {
                project.Status = 2;
            }

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
