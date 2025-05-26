using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectTypeQuery
    {
        List<ProjectType> GetAll();
    }
}
