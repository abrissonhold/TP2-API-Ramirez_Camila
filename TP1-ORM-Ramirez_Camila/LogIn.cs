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
            Console.Clear();
            Console.WriteLine("\n                           Iniciar Sesión                                \n");
            Console.WriteLine("---------------------------------------------------------------------------n");            
            
            User? user = null;
            var userService = new UserService(new UserQuery(context));

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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine($"Bienvenido, {user.Name} (Rol: {user.ApproverRole.Name})\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            return user;
        }
    }
}
