using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command
{
    public class ProjectProposalCommand : IProjectProposalCommand
    {
        private readonly AppDbContext _context;

        public ProjectProposalCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectProposal> CreateProjectProposal(ProjectProposal projectProposal)
        {
            _context.ProjectProposal.Add(projectProposal);
            await _context.SaveChangesAsync();
            return _context.ProjectProposal
                .Include(p => p.AreaDetail)
                .Include(p => p.ProjectType)
                .Include(p => p.ApprovalStatus)
                .Include(p => p.CreatedByUser)
                    .ThenInclude(u => u.ApproverRole)
                .First(p => p.Id == projectProposal.Id);
        }
    }
}
