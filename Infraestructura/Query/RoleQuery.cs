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
    public class RoleQuery : IRoleQuery
    {
        private readonly AppDbContext _context;
        public RoleQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<GenericResponse> GetAll()
        {
            return _context.ApproverRole
                .Select(r => new GenericResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToList();
        }
    }
}
