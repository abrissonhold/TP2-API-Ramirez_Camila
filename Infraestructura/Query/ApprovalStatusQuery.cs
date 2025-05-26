using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class ApprovalStatusQuery : IApprovalStatusQuery
    {
        private readonly AppDbContext _context;
        public ApprovalStatusQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<ApprovalStatus> GetAll()
        {
            return _context.ApprovalStatus.ToList();
        }
    }
}
