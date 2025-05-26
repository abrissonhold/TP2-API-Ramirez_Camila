using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class RoleQuery : IRoleQuery
    {
        private readonly AppDbContext _context;
        public RoleQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<ApproverRole> GetAll()
        {
            return _context.ApproverRole.ToList();
        }
    }
}
