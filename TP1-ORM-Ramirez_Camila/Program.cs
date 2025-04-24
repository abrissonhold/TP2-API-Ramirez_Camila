using Domain.Entities;
using Infrastructure.Persistence;
using TP1_ORM_Ramirez_Camila;

Console.ForegroundColor = ConsoleColor.DarkGreen;
var context = new AppDbContext();
var usuario = await LogIn.IniciarSesion(context);

while (true)
{
    (bool continuar, User nuevoUsuario) = await Menu(context, usuario);
    usuario = nuevoUsuario;
    if (!continuar) break;
    Console.Clear();
}

static async Task<(bool Continuar, User user)> Menu(AppDbContext context, User user)
{
    Console.Clear();
    Console.WriteLine($"Bienvenido, {user.Name} ({user.ApproverRole.Name})\n");
    Console.WriteLine("*---------------------------------------------------------------------------*");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("               Sistema de Aprobación de Proyectos                            ");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("                     1. Crear nueva solicitud                                ");
    Console.WriteLine("                     2. Aprobar o rechazar paso                              ");
    Console.WriteLine("                     3. Ver estado de un proyecto                            ");
    Console.WriteLine("                     4. Cambiar de usuario                                   "); 
    Console.WriteLine("                     5. Salir del sistema                                    ");
    Console.WriteLine("                                                                             ");
    Console.WriteLine("*---------------------------------------------------------------------------*");
    Console.Write("\nIngrese una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            await Opcion1.CrearSolicitud(context, user);
            break;
        case "2":
            await Opcion2.AprobarORechazarPaso(context, user);
            break;
        case "3":
            await Opcion3.VerEstado(context, user);
            break;
        case "4":
            var nuevoUsuario = await LogIn.IniciarSesion(context);
            return (true, nuevoUsuario);
        case "5":
            Console.Clear();
            Console.WriteLine("*---------------------------------------------------------------------------*");
            Console.WriteLine("\n          Gracias por usar nuestro sistema. ¡Hasta la próxima!           \n");
            Console.WriteLine("*---------------------------------------------------------------------------*");
            return (false, user);
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOpción inválida. Por favor, elija una opción del 1 al 4.");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            break;
    }

    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
    return (true, user);
}
