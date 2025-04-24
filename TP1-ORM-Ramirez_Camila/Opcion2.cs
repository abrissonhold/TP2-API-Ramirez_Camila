using Application.Exceptions;
using Application.UserCase;
using Domain.Entities;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;

namespace TP1_ORM_Ramirez_Camila
{
    public class Opcion2
    {
        public static async Task AprobarORechazarPaso(AppDbContext context, User user)
        {
            var stepService = new ProjectApprovalStepService(
                new ProjectApprovalStepQuery(context),
                new ProjectApprovalStepCommand(context),
                new ProjectProposalCommand(context)
                );
            var steps = stepService.GetPendingStepsByRole(user.Role);            
            ProjectApprovalStep? selectedStep = null;
            long selectedStepId = 0;
            string decision;            
            


            Console.Clear();
            Console.WriteLine("\n                          Propuestas pendientes                             \n");
            Console.WriteLine("----------------------------------------------------------------------------");

            if (steps.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay pasos pendientes de aprobación para su rol.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return;
            }
            foreach (var step in steps)
            {
                Console.WriteLine($"Paso ID: {step.Id} | Proyecto: {step.ProjectProposal.Title} | Paso #{step.StepOrder} | Estado: #{step.ApprovalStatus.Name}");
            }

            Console.WriteLine("---------------------------------------------------------------------------\n");
            while (selectedStep == null)
            {
                selectedStepId = ConsoleInputHelper.ReadLong("Ingrese el ID del paso que desea procesar: ", 1);
                selectedStep = stepService.GetById(selectedStepId);

                if (selectedStep == null || selectedStep.Status != 1 || selectedStep.ApproverRoleId != user.Role)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Paso inválido o no autorizado.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    selectedStep = null;
                }
            }       
            
            var p = selectedStep.ProjectProposal;
            Console.Clear();
            Console.WriteLine("\n                  Detalles de la propuesta seleccionada                      \n");
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Console.WriteLine($"  Proyecto: {p.Title} ");
            Console.WriteLine($"  Descripción: {p.Description}");
            Console.WriteLine($"  Área: {p.AreaDetail?.Name ?? "-"}             | Tipo: {p.ProjectType?.Name ?? "-"}");
            Console.WriteLine($"  Monto estimado: ${p.EstimatedAmount:N2}");
            Console.WriteLine($"  Fecha de creación: ${p.CreateAt:dd/MM/yyyy}   | Duración estimada: {p.EstimatedDuration} días");
            Console.WriteLine($"  Creado por: {p.CreatedByUser?.Name ?? "N/A"} ({p.CreatedByUser?.Email ?? "-"})");

            do
            {
                decision = ConsoleInputHelper.ReadString("¿Desea aprobar (A), rechazar (R) u observar (O)?: ")!
                    .Trim().ToUpper();

                if (decision != "A" && decision != "R" && decision != "O")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida. Debe ingresar 'A', 'R' u 'O'.");
                    Console.ResetColor();
                }
            } while (decision != "A" && decision != "R" && decision != "O");

            int newStatus = decision == "A" ? 2 : decision == "R" ? 3 : 4;   

            string? obs = ConsoleInputHelper.ReadOptional("Observaciones (opcional): ");

            bool update = await stepService.UpdateProjectApprovalStep(selectedStepId, newStatus, user.Id, obs);

            Console.ForegroundColor = ConsoleColor.Green;
            if (!update)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se pudo actualizar el paso.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return;
            }

            Console.WriteLine($"\n Paso {(decision == "A" ? "aprobado" : decision == "R" ? "rechazado" : "observado")} correctamente.");
        }
    }
}
