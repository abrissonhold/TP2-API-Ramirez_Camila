using Application.Response;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAll();
        Task<UserResponse?> GetByMail(string email);
        bool Exists(string email);
    }
}
