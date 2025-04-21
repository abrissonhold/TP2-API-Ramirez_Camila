using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_ORM_Ramirez_Camila
{
    public class LogIn
    {
        public static async Task IniciarSesion()
        {
            User? user = null;
            while (user == null)
            {
                Console.Clear();
                Console.WriteLine("                    Iniciar Sesión                         ");
                Console.WriteLine("---------------------------------------------------------\n");
                Console.Write("Ingrese su mail: ");

                int userMail = Console.ReadLine();

                user = context.User.FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Usuario no encontrado. Intente de nuevo.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Bienvenido, {user.Name} (Rol: {user.ApproverRole?.Name ?? "N/A"})");
            Console.ResetColor();
            Thread.Sleep(1000);

            return user;
        
            Console.Clear();
            Console.WriteLine("                    Iniciar Sesión                         ");
            Console.WriteLine("---------------------------------------------------------\n");
            Console.Write("Ingrese su mail: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int userId = int.Parse(Console.ReadLine()!);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            var context = new Infrastructure.Persistence.AppDbContext();
            var user = context.User.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Usuario no encontrado.");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nBienvenido, {user.Name} (Rol: {user.ApproverRole?.Name ?? "N/A"})");
            Console.ResetColor();
        }
    }
}
