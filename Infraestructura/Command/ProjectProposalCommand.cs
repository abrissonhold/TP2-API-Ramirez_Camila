using Application.Interfaces;
using Domain.Entities;
using Infraestructura.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Command
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
