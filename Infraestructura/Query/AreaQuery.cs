using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class AreaQuery : IAreaQuery
    {
        private readonly AppDbContext _context;
        public AreaQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<Area> GetAll()
        {
            return _context.Area.ToList();
        }
    }
}
