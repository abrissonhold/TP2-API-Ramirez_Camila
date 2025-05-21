using Application.Interfaces;
using Application.Response;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class AreaQuery : IAreaQuery
    {
        private readonly AppDbContext _context;
        public AreaQuery(AppDbContext context)
        {
            _context = context;
        }

        public List<GenericResponse> GetAll()
        {
            return _context.Area
                .Select(a => new GenericResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                })
                .ToList();
        }
    }
}
