using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class UserQuery : IUserQuery
    {
        private readonly AppDbContext _context;

        public UserQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.User
                .Include(u => u.ApproverRole)
                .ToListAsync();

        }
        public async Task<User?> GetByMail(string email)
        {
            return await _context.User
                .Include(u => u.ApproverRole)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public bool Exists(string email)
        {
            return _context.User
                .Any(u => u.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.User
                .Include(u => u.ApproverRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
