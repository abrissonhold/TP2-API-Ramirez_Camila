using Application.Interfaces;
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
        public User? GetById(int id)
        {
            return _context.User
                .Include(u => u.ApproverRole)
                .FirstOrDefault(u => u.Id == id);
        }

        public User? GetByMail(string email)
        {
            return _context.User
                .Include(u => u.ApproverRole)
                .FirstOrDefault(u => u.Email == email);
        }

        public bool Exists(string email)
        {
            return _context.User.Any(u => u.Email == email);
        }
    }
}
