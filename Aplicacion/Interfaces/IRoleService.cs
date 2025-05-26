using Application.Response;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
