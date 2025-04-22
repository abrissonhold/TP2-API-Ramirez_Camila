using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepService
    {
        ProjectApprovalStep? GetById(long stepId);
        Task<bool> UpdateProjectApprovalStep(long selectedStepId, int decision, int userId, string? obs);
        List<ProjectApprovalStep> GetPendingStepsByRole(int roleId);
    }
}
