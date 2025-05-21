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
    public class ProjectTypeQuery : IProjectTypeQuery
    {
        private readonly AppDbContext _context;
        public ProjectTypeQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<GenericResponse> GetAll()
        {
            return _context.ProjectType
                .Select(pt => new GenericResponse
                {
                    Id = pt.Id,
                    Name = pt.Name,
                })
                .ToList();
        }
    }
}
