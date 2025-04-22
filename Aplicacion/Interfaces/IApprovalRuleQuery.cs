using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApprovalRuleQuery
    {
        public List<ApprovalRule> GetApplicableRule(ProjectProposal projectProposal);
    }
}
