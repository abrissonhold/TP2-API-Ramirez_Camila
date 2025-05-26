using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ProjectProposalQuery : IProjectProposalQuery
    {
        private readonly AppDbContext _context;
        public ProjectProposalQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectProposal>> GetByFilters(string? title, int? status, int? createdBy, int? approverUser)
        {
            IQueryable<ProjectProposal> query = _context.ProjectProposal
                .Include(proposal => proposal.AreaDetail)
                .Include(proposal => proposal.ProjectType)
                .Include(proposal => proposal.ApprovalStatus)
                .Include(proposal => proposal.CreatedByUser).ThenInclude(u => u.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApprovalStatus)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverUser)
                .AsQueryable().Where(proposal => (title == null || proposal.Title.Contains(title)) &&
                                          (status == null || proposal.Status == status) &&
                                          (createdBy == null || proposal.CreatedBy == createdBy) &&
                                          (approverUser == null || proposal.ProjectApprovalSteps.Any(s => s.ApproverRoleId == approverUser)));

            return await query.OrderByDescending(p => p.CreateAt).ToListAsync();
        }
        public async Task<List<ProjectProposal>> GetByCreatorId(int userId)
        {
            return await _context.ProjectProposal
                .Include(proposal => proposal.AreaDetail)
                .Include(proposal => proposal.ProjectType)
                .Include(proposal => proposal.ApprovalStatus)
                .Include(proposal => proposal.CreatedByUser).ThenInclude(u => u.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApprovalStatus)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverUser)
                .Where(proposal => proposal.CreatedBy == userId)
                .OrderByDescending(proposal => proposal.CreateAt)
                .ToListAsync();
        }
        public async Task<ProjectProposal> GetById(Guid id)
        {
            return await _context.ProjectProposal
                .Include(proposal => proposal.AreaDetail)
                .Include(proposal => proposal.ProjectType)
                .Include(proposal => proposal.ApprovalStatus)
                .Include(proposal => proposal.CreatedByUser).ThenInclude(u => u.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverRole)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApprovalStatus)
                .Include(proposal => proposal.ProjectApprovalSteps).ThenInclude(s => s.ApproverUser)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool ExistsByTitle(string title)
        {
            return _context.ProjectProposal
                .Any(p => p.Title == title);
        }
    }
}
