using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_ORM_Ramirez_Camila
{
    public class Opcion2
    {
        public static async Task AprobarORechazarPaso(AppDbContext context)
        {
            Console.Clear();
            Console.WriteLine("Para continuar debe iniciar sesion");
            Console.ResetColor();

            Console.Write("Ingrese su ID de usuario: ");
            int userId = int.Parse(Console.ReadLine()!);

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

            // Buscar pasos pendientes que coincidan con el rol del usuario
            var steps = context.ProjectApprovalStep
                .Where(s => s.ApproverRoleId == user.Role && s.Status == 1)
                .OrderBy(s => s.StepOrder)
                .ToList();

            if (!steps.Any())
            {
                Console.WriteLine("\n✅ No hay pasos pendientes de aprobación para su rol.");
                return;
            }

            Console.WriteLine("\n📋 Pasos pendientes:");
            foreach (var step in steps)
            {
                Console.WriteLine($"🧾 Paso ID: {step.Id} | Proyecto: {step.ProjectProposal.Title} | Paso: {step.StepOrder}");
            }

            Console.Write("\nIngrese el ID del paso que desea procesar: ");
            long pasoId = long.Parse(Console.ReadLine()!);

            var pasoSeleccionado = steps.FirstOrDefault(s => s.Id == pasoId);

            if (pasoSeleccionado == null)
            {
                Console.WriteLine("❌ Paso inválido.");
                return;
            }

            Console.Write("¿Desea aprobar (A) o rechazar (R) este paso?: ");
            string decision = Console.ReadLine()!.Trim().ToUpper();

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
