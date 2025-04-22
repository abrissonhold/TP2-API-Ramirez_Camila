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
        public static async Task AprobarORechazarPaso(AppDbContext context, int userId)
        {
            Console.Clear();
            Console.WriteLine("\n                           Pasos pendientes                             \n");
            Console.WriteLine("---------------------------------------------------------------------------n");

            var user = new UserService(new UserQuery(context)).GetById(userId);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Usuario no encontrado.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return;
            }

            var stepService = new ProjectApprovalStepService(
                new ProjectApprovalStepQuery(context),
                new ProjectApprovalStepCommand(context));

            var steps = stepService.GetPendingStepsByRole(user.Role);
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

            ProjectApprovalStep? selectedStep = null;
            long selectedStepId = 0;
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

            string decision;
            do
            {
                decision = ConsoleInputHelper.ReadString("¿Desea aprobar (A) o rechazar (R) este paso?: ")!.Trim().ToUpper();
                if (decision != "A" && decision != "R")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción inválida. Debe ingresar 'A' o 'R'.");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return;
                }
            } while (decision != "A" && decision != "R");

            int newStatus = (decision == "A") ? 2 : 3;

            string? obs = ConsoleInputHelper.ReadOptional("Observaciones (opcional): ");

            bool update = await stepService.UpdateProjectApprovalStep(selectedStepId, newStatus, userId, obs);

            Console.ForegroundColor = ConsoleColor.Green;
            if (!update)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se pudo actualizar el paso.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return;
            }

            Console.WriteLine($"\n Paso {(decision == "A" ? "aprobado" : "rechazado")} correctamente.");
        }
    }
}
