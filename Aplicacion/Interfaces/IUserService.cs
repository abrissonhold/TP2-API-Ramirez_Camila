using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User? GetById(int id);
        User? GetByMail(string email);
        bool Exists(string email);
    }
}
