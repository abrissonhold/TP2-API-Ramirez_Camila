using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserQuery
    {
        Task<List<User>> GetAll();
        Task<User?> GetByMail(string email);
        Task<bool> Exists(int userId);
        bool ExistsByEmail(string email);
        Task<User?> GetById(int id);
    }
}
