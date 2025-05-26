using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRoleQuery
    {
        List<ApproverRole> GetAll();
    }
}
