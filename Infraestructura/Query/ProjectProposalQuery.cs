using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class ProjectProposalQuery : IProjectProposalQuery
    {
        private readonly AppDbContext _context;
        public ProjectProposalQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<ProjectProposal> GetByCreatorId(int userId)
        {
            return _context.ProjectProposal
                .Include(p => p.AreaDetail)
                .Include(p => p.ProjectType)
                .Include(p => p.ApprovalStatus)
                .Include(p => p.CreatedByUser).ThenInclude(u => u.ApproverRole)
                .Include(p => p.ProjectApprovalSteps).ThenInclude(s => s.ApproverRole)
                .Include(p => p.ProjectApprovalSteps).ThenInclude(s => s.ApprovalStatus)
                .Include(p => p.ProjectApprovalSteps).ThenInclude(s => s.ApproverUser)
                .Where(p => p.CreatedBy == userId)
                .OrderByDescending(p => p.CreateAt)
                .ToList();
        }
    }
}
