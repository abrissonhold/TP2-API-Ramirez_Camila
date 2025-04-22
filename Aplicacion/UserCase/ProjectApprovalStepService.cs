using Application.Interfaces;
using Domain.Entities;

namespace Application.UserCase
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IProjectApprovalStepQuery _query;
        private readonly IProjectApprovalStepCommand _command;
        public ProjectApprovalStepService(IProjectApprovalStepQuery query, IProjectApprovalStepCommand command)
        {
            _query = query;
            _command = command;
        }

        public ProjectApprovalStep? GetById(long stepId)
        {
            return _query.GetById(stepId);
        }
        public async Task<bool> UpdateProjectApprovalStep(long selectedStepId, int decision, int userId, string? obs)
        {
            var selectedApprovalStep = GetById(selectedStepId);
            selectedApprovalStep.Status = decision;
            selectedApprovalStep.DecisionDate = DateTime.Now;
            selectedApprovalStep.ApproverUserId = userId;
            selectedApprovalStep.Observations = obs;

            return await _command.UpdateStep(selectedApprovalStep);
        }
        public List<ProjectApprovalStep> GetPendingStepsByRole(int roleId)
        {
            return _query.GetPendingStepsByRole(roleId);
        }
    }
}
