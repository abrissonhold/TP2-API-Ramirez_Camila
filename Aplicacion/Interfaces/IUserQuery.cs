using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserQuery
    {
        List<User> GetAll();
        User? GetByMail(string email);
        bool Exists(string email);
    }
}
