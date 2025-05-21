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
    public class ApprovalStatusQuery : IApprovalStatusQuery
    {
        private readonly AppDbContext _context;
        public ApprovalStatusQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<ApprovalStatus> GetAll()
        {
            return _context.ApprovalStatus.ToList();
        }
    }
}
