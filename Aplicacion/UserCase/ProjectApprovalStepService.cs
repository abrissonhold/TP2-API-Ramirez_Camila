using Application.Interfaces;
using Domain.Entities;

namespace Application.UserCase
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IProjectApprovalStepQuery _query;
        private readonly IProjectApprovalStepCommand _command;
        private readonly IProjectProposalCommand _projectCommand;
        public ProjectApprovalStepService(IProjectApprovalStepQuery query, IProjectApprovalStepCommand command, IProjectProposalCommand projectProposalCommand)
        {
            _query = query;
            _command = command;
            _projectCommand = projectProposalCommand;
        }

        public ProjectApprovalStep? GetById(long stepId)
        {
            return _query.GetById(stepId);
        }
        public async Task<bool> UpdateProjectApprovalStep(long stepId, int newStatus, int userId, string? obs)
        {
            var step = GetById(stepId);

            step.Status = newStatus;
            step.DecisionDate = DateTime.Now;
            step.ApproverUserId = userId;
            step.Observations = obs;
            var actualizado = await _command.UpdateStep(step);
            if (!actualizado) return false;

            var project = step.ProjectProposal;
            var allSteps = project.ProjectApprovalSteps;
            if (newStatus == 3) project.Status = 3;
            if (allSteps.All(s => s.Status == 2))  project.Status = 2;
            await _projectCommand.UpdateProjectProposalStatus(project);

            return true;
        }
        public List<ProjectApprovalStep> GetPendingStepsByRole(int approverRoleId)
        {
            var allSteps = _query.GetPendingStepsByRole(approverRoleId);

            var valid = new List<ProjectApprovalStep>();

            foreach (var step in allSteps)
            {
                var previousSteps = step.ProjectProposal.ProjectApprovalSteps
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
