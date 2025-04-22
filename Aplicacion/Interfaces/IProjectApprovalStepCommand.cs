using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepCommand
    {
        public Task CreateProjectApprovalStep(ProjectProposal projectProposal, List<ApprovalRule> rules);
        public Task<bool> UpdateStep(ProjectApprovalStep step);
    }
}
