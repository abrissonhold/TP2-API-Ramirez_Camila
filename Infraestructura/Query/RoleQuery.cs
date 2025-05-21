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
