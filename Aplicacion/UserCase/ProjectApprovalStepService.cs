using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCase
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IProjectApprovalStepQuery _query;
        public ProjectApprovalStepService(IProjectApprovalStepQuery query)
        {
            _query = query;
        }

        public ProjectApprovalStep? GetById(long stepId)
        {
            return _query.GetById(stepId);
        }

        public List<ProjectApprovalStep> GetPendingStepsByRole(int roleId)
        {
            return _query.GetPendingStepsByRole(roleId);
        }
    }
}
