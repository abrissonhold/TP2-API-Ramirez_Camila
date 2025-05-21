using Application.Interfaces;
using Application.Mappers;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
