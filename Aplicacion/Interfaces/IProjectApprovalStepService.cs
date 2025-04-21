using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectApprovalStepService
    {
        ProjectApprovalStep? GetById(long stepId);
        List<ProjectApprovalStep> GetPendingStepsByRole(int roleId);
    }
}
