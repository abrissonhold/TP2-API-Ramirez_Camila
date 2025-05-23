using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command
{
    public class ProjectApprovalStepCommand : IProjectApprovalStepCommand
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateProjectApprovalStep(ProjectProposal projectProposal, List<ApprovalRule> rules)
        {
            int orden = 1;
            foreach (var rule in rules)
            {
                var pendingStatus = await _context.ApprovalStatus.FirstAsync(x => x.Id == 1);
                var step = new ProjectApprovalStep
                {
                    ProjectProposalId = projectProposal.Id,
                    ProjectProposal = projectProposal,
                    ApproverUserId = null,
                    ApproverUser = null,
                    ApproverRoleId = rule.ApproverRoleId,
                    ApproverRole = rule.ApproverRole,
                    Status = 1,
                    ApprovalStatus = pendingStatus,
                    StepOrder = orden,
                    DecisionDate = null,
                    Observations = null
                };
                 _context.ProjectApprovalStep.Add(step);
                orden++;
            }
            await _context.SaveChangesAsync();
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
