using Application.Interfaces;
using Domain.Entities;
using Infraestructura.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Command
{
    public class ProjectApprovalStepCommand : IProjectApprovalStepCommand
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepCommand(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateProjectApprovalStep(ProjectProposal projectProposal, List<ApprovalRule> rules)
        {
            int orden = 1;
            foreach (var rule in rules)
            {
                var approverUser = _context.User
                    .Where(u => u.Role == rule.ApproverRoleId)
                    .OrderBy(_ => Guid.NewGuid())
                    .FirstOrDefault();
                var step = new ProjectApprovalStep
                {
                    ProjectProposalId = projectProposal.Id,
                    ProjectProposal = projectProposal,
                    ApproverUserId = approverUser?.Id,
                    ApproverUser = approverUser,
                    ApproverRoleId = rule.ApproverRoleId,
                    ApproverRole = rule.ApproverRole,
                    Status = 1,
                    ApprovalStatus = _context.ApprovalStatus
                    .First(x => x.Id == 1),
                    StepOrder = orden,
                    DecisionDate = null, //Implementar la lógica para la fecha de decisión
                    Observations = null //Implementar la lógica para las observaciones
                };
                _context.ProjectApprovalStep.Add(step);
            }
            return _context.SaveChangesAsync();
        }
    }
}
