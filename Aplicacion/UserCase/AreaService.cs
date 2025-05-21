using Application.Interfaces;
using Application.Mappers;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCase
{
    public class AreaService : IAreaService
    {
        private readonly IAreaQuery _query;

        public AreaService(IAreaQuery query)
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
