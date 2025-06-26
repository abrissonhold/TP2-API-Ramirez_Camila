using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAreaQuery
    {
        Task<bool> Exists(int areaId);
        List<Area> GetAll();
    }
}
