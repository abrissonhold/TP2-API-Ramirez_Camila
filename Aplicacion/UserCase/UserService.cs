using Application.Interfaces;
using Application.Mappers;
using Application.Response;
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

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _query.GetAll();
            return UserMapper.ToResponseList(users);
        }        
        public async Task<UserResponse?> GetByMail(string email)
        {
            User user = await _query.GetByMail(email);
            return user != null ? UserMapper.ToResponse(user) : null;
        }
        public bool Exists(string email)
        {
            return _query.Exists(email);
        }
        public async Task<UserResponse?> GetById(int id)
        {
            User user = await _query.GetById(id);
            return user != null ? UserMapper.ToResponse(user) : null;
        }
    }
}
