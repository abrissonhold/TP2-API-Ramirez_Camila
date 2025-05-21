using Application.Interfaces;
using Application.Response;
using Domain.Entities;
using System.Threading.Tasks;

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
            List<User> users = _query.GetAll();
            return users.Select(users => new UserResponse
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Role = users.Role,
                ApproverRole = new GenericResponse
                {
                    Id = users.ApproverRole.Id,
                    Name = users.ApproverRole.Name
                },
            }).ToList();
        }        
        public UserResponse? GetByMail(string email)
        {
            User user = _query.GetByMail(email);
            return 
                new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    ApproverRole = new GenericResponse
                    {
                        Id = user.ApproverRole.Id,
                        Name = user.ApproverRole.Name
                    },
                };
        }
        public bool Exists(string email)
        {
            return _query.Exists(email);
        }


    }
}
