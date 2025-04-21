using Application.UserCase;
using Infrastructure.Persistence;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_ORM_Ramirez_Camila
{
    public class Opcion2
    {
        public static async Task AprobarORechazarPaso(AppDbContext context, int userId)
        {
            Console.Clear();
            var userService = new UserService(new UserQuery(context));
            var user = userService.GetById(userId);

            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Usuario no encontrado.");
                Console.ResetColor();
                return;
            }

            var stepService = new ProjectApprovalStepService(new ProjectApprovalStepQuery(context));
            var steps = stepService.GetPendingStepsByRole(user.Role);

            if (steps.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay pasos pendientes de aprobación para su rol.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\nPasos pendientes:");

            foreach (var step in steps)
            {
                Console.WriteLine($"Paso ID: {step.Id} | Proyecto: {step.ProjectProposal.Title} | Paso #{step.StepOrder} | Estado: #{step.ApprovalStatus.Name}");
            }

            Console.Write("\nIngrese el ID del paso que desea procesar: ");
            if (!long.TryParse(Console.ReadLine(), out long pasoId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ingrese un ID válido.");
                Console.ResetColor();
                return;
            }

            var pasoSeleccionado = stepService.GetById(pasoId);

            if (pasoSeleccionado == null || pasoSeleccionado.Status != 1 || pasoSeleccionado.ApproverRoleId != user.Role)
            {
                Console.WriteLine("❌ Paso inválido o no autorizado.");
                return;
            }

            Console.Write("¿Desea aprobar (A) o rechazar (R) este paso?: ");
            string decision = Console.ReadLine()!.Trim().ToUpper();

            if (decision != "A" && decision != "R")
            {
                Console.WriteLine("❌ Opción inválida. Debe ingresar 'A' o 'R'.");
                return;
            }

            Console.Write("Observaciones (opcional): ");
            string? obs = Console.ReadLine();

            pasoSeleccionado.Status = (decision == "A") ? 2 : 3;
            pasoSeleccionado.DecisionDate = DateTime.Now;
            pasoSeleccionado.ApproverUserId = user.Id;
            pasoSeleccionado.Observations = obs;

            await context.SaveChangesAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Paso {(decision == "A" ? "aprobado" : "rechazado")} correctamente.");
            Console.ResetColor();
        }
    }

}
