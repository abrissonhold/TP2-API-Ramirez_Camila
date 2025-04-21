using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectProposalService
    {
        public Task<ProjectProposalResponse> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy);
    }
}
