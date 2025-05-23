using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ApprovalRuleQuery : IApprovalRuleQuery
    {
        private readonly AppDbContext _context;
        public ApprovalRuleQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<ApprovalRule> GetApplicableRule(ProjectProposal projectProposal)
        {
            List<ApprovalRule> applicableRules = _context.ApprovalRule
                .Include(r => r.ApproverRole)
                .Include(r => r.AreaDetail)
                .Include(r => r.ProjectType)
                .Where(r =>
                    (r.Type == null || r.Type == projectProposal.Type) &&
                    (r.Area == null || r.Area == projectProposal.Area) &&
                    (projectProposal.EstimatedAmount >= r.MinAmount) &&
                    (r.MaxAmount == 0 || projectProposal.EstimatedAmount <= r.MaxAmount)
                )
                .ToList();

            List<ApprovalRule> FilteredApprovalRules = applicableRules
                .GroupBy(r => r.StepOrder)
                .Select(groupedRules => groupedRules
                    .OrderByDescending(r =>
                        (r.Type != null ? 1 : 0) +
                        (r.Area != null ? 1 : 0) +
                        (r.MinAmount > 0 ? 1 : 0) +
                        (r.MaxAmount > 0 ? 1 : 0)
                    )
                    .First()
                )
                .OrderBy(r => r.StepOrder)
                .ToList();

            return FilteredApprovalRules;
        }

    }
}
