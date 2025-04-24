using Application.Exceptions;
using Application.UserCase;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Query;

namespace TP1_ORM_Ramirez_Camila
{
    public class LogIn
    {
        public static async Task<User> IniciarSesion(AppDbContext context)
        {
            var userService = new UserService(new UserQuery(context));            
            User? user = null;            

            Console.Clear();
            Console.WriteLine("\n                           Iniciar Sesión                                \n");
            Console.WriteLine("----------------------------------------------------------------------------");            
            
            while (user == null)
            {
                string email = ConsoleInputHelper.ReadEmail("Ingrese su correo: ");
                user = userService.GetByMail(email);

                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Usuario no encontrado. Intente nuevamente.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
            }
            return user;
        }
    }
}
