using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepCommand
    {
        Task CreateProjectApprovalStep(ProjectProposal projectProposal, List<ApprovalRule> rules);
        Task<bool> UpdateStep(ProjectApprovalStep step);
    }
}
