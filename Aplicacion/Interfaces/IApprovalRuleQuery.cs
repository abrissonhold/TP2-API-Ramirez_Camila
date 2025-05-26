using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApprovalRuleQuery
    {
        List<ApprovalRule> GetApplicableRule(ProjectProposal projectProposal);
    }
}
