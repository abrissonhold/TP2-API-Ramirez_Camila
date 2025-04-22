using Application.Interfaces;
using Domain.Entities;

namespace Application.UserCase
{
    public class UserService : IUserService
    {
        private readonly IUserQuery _query;

        public UserService(IUserQuery query)
        {
            _query = query;
        }

        public User? GetById(int id)
        {
            return _query.GetById(id);
        }
        public User? GetByMail(string email)
        {
            return _query.GetByMail(email);
        }
        public bool Exists(string email)
        {
            return _query.Exists(email);
        }
    }
}
