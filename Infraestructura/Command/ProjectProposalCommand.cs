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
    public class ProjectProposalCommand : IProjectProposalCommand
    {
        private readonly AppDbContext _context;

        public ProjectProposalCommand(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateProjectProposal(ProjectProposal projectProposal)
        {
            _context.ProjectProposal.Add(projectProposal);
            return _context.SaveChangesAsync();
        }
    }
}
