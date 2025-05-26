using Application.Interfaces;
using Application.Mappers;
using Application.Response;

namespace Application.UserCase
{
    public class ProjectTypeService : IProjectTypeService
    {
        private readonly IProjectTypeQuery _query;
        public ProjectTypeService(IProjectTypeQuery query)
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
