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
            foreach (ApprovalRule rule in rules)
            {
                ApprovalStatus pendingStatus = await _context.ApprovalStatus.FirstAsync(x => x.Id == 1);
                ProjectApprovalStep step = new()
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
                _ = _context.ProjectApprovalStep.Add(step);
                orden++;
            }
            _ = await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStep(ProjectApprovalStep step)
        {
            ProjectApprovalStep? existingStep = await _context.ProjectApprovalStep.FindAsync(step.Id);
            if (existingStep == null)
            {
                return false;
            }

            existingStep.Status = step.Status;
            existingStep.Observations = step.Observations;
            existingStep.DecisionDate = step.DecisionDate;
            existingStep.ApproverUserId = step.ApproverUserId;

            _ = await _context.SaveChangesAsync();
            return true;
        }
        public async Task DeleteStepsByProposal(Guid proposalId)
        {
            IQueryable<ProjectApprovalStep> steps = _context.ProjectApprovalStep.Where(s => s.ProjectProposalId == proposalId);
            _context.ProjectApprovalStep.RemoveRange(steps);
            _ = await _context.SaveChangesAsync();
        }

    }
}
