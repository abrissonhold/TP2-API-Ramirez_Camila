using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectTypeQuery
    {
        Task<bool> Exists(int typeId);
        List<ProjectType> GetAll();
    }
}
