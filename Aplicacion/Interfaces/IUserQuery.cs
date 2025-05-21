using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserQuery
    {
        Task<List<User>> GetAll();
        Task<User?> GetByMail(string email);
        bool Exists(string email);
    }
}
