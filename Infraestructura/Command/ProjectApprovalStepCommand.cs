using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
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
                var step = new ProjectApprovalStep
                {
                    ProjectProposalId = projectProposal.Id,
                    ProjectProposal = projectProposal,
                    ApproverUserId = null,
                    ApproverUser = null,
                    ApproverRoleId = rule.ApproverRoleId,
                    ApproverRole = rule.ApproverRole,
                    Status = 1,
                    ApprovalStatus = _context.ApprovalStatus
                    .First(x => x.Id == 1),
                    StepOrder = orden,
                    DecisionDate = null, 
                    Observations = null
                };
                _context.ProjectApprovalStep.Add(step);
                orden++;
            }
            return _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStep(ProjectApprovalStep step)
        {
            var existingStep = await _context.ProjectApprovalStep.FindAsync(step.Id);
            if (existingStep == null) return false;

            existingStep.Status = step.Status;
            existingStep.Observations = step.Observations;
            existingStep.DecisionDate = step.DecisionDate;
            existingStep.ApproverUserId = step.ApproverUserId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
