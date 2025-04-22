using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepQuery
    {
        ProjectApprovalStep? GetById(long stepId);
        List<ProjectApprovalStep> GetPendingStepsByRole(int approverRoleId);
    }
}
