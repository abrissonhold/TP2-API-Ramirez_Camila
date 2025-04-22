using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepCommand
    {
        public Task CreateProjectApprovalStep(ProjectProposal projectProposal, List<ApprovalRule> rules);
        public Task<bool> UpdateStep(ProjectApprovalStep step);
    }
}
