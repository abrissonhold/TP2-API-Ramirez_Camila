using Application.Interfaces;
using Application.Response;
using Domain.Entities;
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

        public List<Area> GetAll()
        {
            return _context.Area.ToList();
        }
    }
}
