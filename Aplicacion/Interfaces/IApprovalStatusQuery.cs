using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApprovalStatusQuery
    {
        List<ApprovalStatus> GetAll();
    }
}
