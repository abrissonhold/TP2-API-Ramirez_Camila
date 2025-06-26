using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class AreaQuery : IAreaQuery
    {
        private readonly AppDbContext _context;
        public AreaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Exists(int areaId)
        {
            return await _context.Area.AnyAsync(a => a.Id == areaId);
        }
        public List<Area> GetAll()
        {
            return _context.Area.ToList();
        }
    }
}
