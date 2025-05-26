using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponse? ToResponse(User user)
        {
            return user == null
                ? null
                : new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    ApproverRole = GenericMapper.ToResponse(user.ApproverRole),
                };
        }

        public static List<UserResponse> ToResponseList(List<User> users)
        {
            return users.Select(ToResponse).ToList();
        }
    }
}
