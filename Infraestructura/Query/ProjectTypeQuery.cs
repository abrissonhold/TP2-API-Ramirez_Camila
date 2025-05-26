using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class ProjectTypeQuery : IProjectTypeQuery
    {
        private readonly AppDbContext _context;
        public ProjectTypeQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<ProjectType> GetAll()
        {
            return _context.ProjectType.ToList();
        }
    }
}
