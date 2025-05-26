using Application.Response;

namespace Application.Interfaces
{
    public interface IAreaService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
