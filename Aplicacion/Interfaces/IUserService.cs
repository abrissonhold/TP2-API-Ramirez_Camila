using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User? GetById(int id);
        User? GetByMail(string email);
        bool Exists(string email);
    }
}
