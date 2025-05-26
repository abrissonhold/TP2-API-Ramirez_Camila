using Application.Response;

namespace Application.Interfaces
{
    public interface IApprovalStatusService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
