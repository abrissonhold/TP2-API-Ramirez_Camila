using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ProjectApprovalStepQuery : IProjectApprovalStepQuery
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepQuery(AppDbContext context)
        {
            _context = context;
        }

        public ProjectApprovalStep? GetById(long stepId)
        {
            return _context.ProjectApprovalStep
                .Include(s => s.ProjectProposal)
                .Include(s => s.ApprovalStatus)
                .FirstOrDefault(s => s.Id == stepId);
        }

        public List<ProjectApprovalStep> GetPendingStepsByRole(int approverRoleId)
        {
            return _context.ProjectApprovalStep
                .Include(p => p.ProjectProposal)
                .Include(p => p.ApprovalStatus)
                .Where(s => s.ApproverRoleId == approverRoleId && s.Status == 1)
                .OrderBy(s => s.StepOrder)
                .ToList();
        }
    }
}
