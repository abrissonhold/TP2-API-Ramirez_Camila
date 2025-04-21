using Application.Request;
using Application.UserCase;
using Domain.Entities;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using TP1_ORM_Ramirez_Camila;

Console.ForegroundColor = ConsoleColor.DarkGreen;
var context = new AppDbContext();
int? actualUser = null;

while (await Menu(context))
{
    Console.Clear();
}

static async Task<bool> Menu(AppDbContext context)
{
    Console.WriteLine("*---------------------------------------------------------------------------*");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("               Sistema de Aprobación de Proyectos                            ");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("                     1. Crear nueva solicitud                                ");
    Console.WriteLine("                     2. Aprobar o rechazar paso                              ");
    Console.WriteLine("                     3. Ver estado de un proyecto                            ");
    Console.WriteLine("                     4. Salir del sistema                                    ");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("*---------------------------------------------------------------------------*");
    Console.Write("\nIngrese una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            await Opcion1.CrearSolicitud(context, actualUser);
            break;
        case "2":
            await Opcion2.AprobarPaso(context, actualUser);
            break;
        case "3":
            //await Opcion3.VerEstado(context);
            break;
        case "4":
            Console.Clear();
            Console.WriteLine("*---------------------------------------------------------------------------*");
            Console.WriteLine("\n          Gracias por usar nuestro sistema. ¡Hasta la próxima!           \n");
            Console.WriteLine("*---------------------------------------------------------------------------*");
            return false;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOpción inválida. Por favor, elija una opción del 1 al 4.");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            break;
    }

    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
    return true;
}
