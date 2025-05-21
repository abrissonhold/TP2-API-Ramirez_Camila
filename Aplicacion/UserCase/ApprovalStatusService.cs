using Application.Interfaces;
using Application.Response;
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
            return Task.FromResult(_query.GetAll());
        }
    }
}
