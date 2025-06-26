using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ProjectTypeQuery : IProjectTypeQuery
    {
        private readonly AppDbContext _context;
        public ProjectTypeQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Exists(int typeId)
        {
            return await _context.ProjectType.AnyAsync(t => t.Id == typeId);
        }
        public List<ProjectType> GetAll()
        {
            return _context.ProjectType.ToList();
        }
    }
}
