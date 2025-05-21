using Application.Interfaces;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Task.FromResult(_query.GetAll());
        }
    }
}
