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

        public List<ProjectApprovalStep> GetPendingStepsByRole(int approverRoleId)
        {
            return _context.ProjectApprovalStep
                .Where(s =>
                    s.ApproverRoleId == approverRoleId &&
                    s.Status == 1 &&
                    s.ProjectProposal.Status == 1
                )
                .Include(s => s.ProjectProposal)
                .Include(s => s.ApprovalStatus)
                .Include(s => s.ApproverRole)
                .Include(s => s.ApproverUser)
                .OrderBy(s => s.StepOrder)
                .ToList();
        }
        public async Task<ProjectApprovalStep?> GetById(long stepId)
        {
            return await _context.ProjectApprovalStep
                .Include(s => s.ApproverRole)
                .Include(s => s.ApprovalStatus)
                .Include(s => s.ApproverUser)
               .FirstOrDefaultAsync(s => s.Id == stepId);
        }
    }
}
