using Application.Request;
using Application.UserCase;
using Infraestructura.Command;
using Infraestructura.Persistence;
using Infraestructura.Query;

Console.ForegroundColor = ConsoleColor.DarkGreen;

while (await Menu())
{
    Console.Clear();
}

static async Task<bool> Menu()
{
    Console.WriteLine("*--------------------------------------------------------------*");
    Console.WriteLine("|                                                              |");
    Console.WriteLine("|              Sistema de Aprobación de Proyectos              |");
    Console.WriteLine("|                                                              |");
    Console.WriteLine("|                    1. Crear nueva solicitud                  |");
    Console.WriteLine("|                    2. Aprobar o rechazar paso                |");
    Console.WriteLine("|                    3. Ver estado de un proyecto              |");
    Console.WriteLine("|                    4. Salir del sistema                      |");
    Console.WriteLine("|                                                              |");
    Console.WriteLine("*--------------------------------------------------------------*");
    Console.Write("\nIngrese una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            await CrearSolicitud();
            break;
        case "2":
            //await AprobarPaso();
            break;
        case "3":
            //await VerEstado();
            break;
        case "4":
            Console.WriteLine("\nGracias por usar el sistema 💼. ¡Hasta la próxima!\n");
            return false;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️ Opción inválida. Por favor, elija una opción del 1 al 4.");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            break;
    }

    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
    return true;
}

static async Task CrearSolicitud()
{
    Console.Clear();
    Console.WriteLine("📄 Creación de nueva solicitud de proyecto");
    Console.WriteLine("-------------------------------------------");

    Console.Write("Título del proyecto: ");
    var titulo = Console.ReadLine();

    Console.Write("Descripción: ");
    var descripcion = Console.ReadLine();

    Console.Write("ID del Área (1-Finanzas, 2-Tecnología, 3-RRHH, 4-Operaciones): ");
    int area = int.Parse(Console.ReadLine()!);

    Console.Write("ID del Tipo de Proyecto (1-Mejora, 2-Innovación, 3-Infraestructura, 4-Capacitación): ");
    int tipo = int.Parse(Console.ReadLine()!);

    Console.Write("Monto estimado: $");
    decimal monto = decimal.Parse(Console.ReadLine()!);

    Console.Write("Duración estimada (en meses): ");
    int duracion = int.Parse(Console.ReadLine()!);

    Console.Write("ID del Usuario que crea la solicitud: ");
    int userId = int.Parse(Console.ReadLine()!);

    var request = new ProjectProposalRequest
    {
        Title = titulo!,
        Description = descripcion!,
        Area = area,
        Type = tipo,
        EstimatedAmount = monto,
        EstimatedDuration = duracion,
        CreatedBy = userId
    };

    var context = new AppDbContext();

    var service = new ProjectProposalService(
        new ProjectProposalCommand(context),
        new ApprovalRuleQuery(context),
        new ProjectApprovalStepCommand(context)
    );
    var response = await service.CreateProjectProposal(request);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n✅ Proyecto creado con éxito. ID: {response.Id}");
    Console.ResetColor();
}
