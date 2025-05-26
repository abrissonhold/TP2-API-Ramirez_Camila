using Application.Response;

namespace Application.Interfaces
{
    public interface IProjectTypeService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
