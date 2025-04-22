using Application.Exceptions;
using Application.UserCase;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;

namespace TP1_ORM_Ramirez_Camila
{
    public class Opcion3
    {
        public static async Task VerEstado(AppDbContext context, int userId)
        {
            Console.Clear();
            Console.WriteLine("\n                     Estado de tus proyectos                             \n");
            Console.WriteLine("----------------------------------------------------------------------------");

            var service = new ProjectProposalService(
                new ProjectProposalCommand(context),
                new ProjectProposalQuery(context),
                new ApprovalRuleQuery(context),
                new ProjectApprovalStepCommand(context)
            );

            var projects = service.GetDetail(userId);

            if (projects.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No se encontraron proyectos creados por este usuario.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return;
            }

            foreach (var project in projects)
            {
                var p = project.ProjectProposal;
                Console.WriteLine($"\nProyecto: {p.Title}");
                Console.WriteLine($"Descripción: {p.Description}");
                Console.WriteLine($"Estado: {p.ApprovalStatus.Name}");
                Console.WriteLine($"Fecha de creación: {p.CreateAt:dd / MM / yyyy}");

                if (project.ProjectApprovalSteps.Count > 0)
                {
                    Console.WriteLine("Pasos de aprobación:");
                    foreach (var step in project.ProjectApprovalSteps)
                    {
                        Console.WriteLine($"    Paso {step.StepOrder}");
                        Console.WriteLine($"    Estado: {step.ApprovalStatus.Name}");
                        Console.WriteLine($"    Aprobador: {step.ApproverUser?.Name ?? "Pendiente"} - Rol: {step.ApproverRole.Name}");
                        Console.WriteLine($"    Fecha decisión: {step.DecisionDate?.ToString("dd/MM/yyyy") ?? "N/A"}");
                        Console.WriteLine($"    Observaciones: {step.Observations ?? "-"}\n");
                    }

                    return;
                }
                Console.WriteLine("----------------------------------------------------------------------------");
            }               
            Console.WriteLine("Fin de la consulta.");
        }
    }
}