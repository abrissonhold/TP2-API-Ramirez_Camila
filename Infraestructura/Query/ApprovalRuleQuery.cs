using Application.Interfaces;
using Domain.Entities;
using Infraestructura.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Query
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
            var reglas = _context.ApprovalRule
            .Where(r =>
                (r.Type == null || r.Type == projectProposal.Type) &&
                (r.Area == null || r.Area == projectProposal.Area) &&
                (projectProposal.EstimatedAmount >= r.MinAmount) &&
                (r.MaxAmount == 0 || projectProposal.EstimatedAmount <= r.MaxAmount)
            )
            .ToList();

            var reglasOrdenadas = reglas
                .OrderByDescending(r =>
                    (r.Type != null ? 1 : 0) +
                    (r.Area != null ? 1 : 0) +
                    (r.MinAmount > 0 ? 1 : 0) +
                    (r.MaxAmount > 0 ? 1 : 0)
                )
                .ThenBy(r => r.StepOrder)
                .ToList();
            return reglasOrdenadas;
        }

    }
}
