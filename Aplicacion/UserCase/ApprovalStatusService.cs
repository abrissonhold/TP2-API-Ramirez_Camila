using Application.Interfaces;
using Application.Mappers;
using Application.Response;

namespace Application.UserCase
{
    public class ApprovalStatusService : IApprovalStatusService
    {
        private readonly IApprovalStatusQuery _query;
        public ApprovalStatusService(IApprovalStatusQuery query)
        {
            _query = query;
        }
        public Task<List<GenericResponse>> GetAll()
        {
            List<GenericResponse> response = GenericMapper.ToResponseList(_query.GetAll());
            return Task.FromResult(response);
        }
    }
}
