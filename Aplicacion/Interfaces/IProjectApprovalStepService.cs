﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepService
    {
        Task<ProjectApprovalStep?> GetById(long stepId);
        Task<bool> UpdateProjectApprovalStep(long selectedStepId, int decision, int userId, string? obs);
        List<ProjectApprovalStep> GetPendingStepsByRole(int roleId);
    }
}
