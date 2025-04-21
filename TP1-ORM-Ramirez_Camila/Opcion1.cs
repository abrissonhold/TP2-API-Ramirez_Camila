using Application.Interfaces;
using Application.Request;
using Application.UserCase;
using Domain.Entities;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_ORM_Ramirez_Camila
{
    public class Opcion1
    {
        public static async Task CrearSolicitud(AppDbContext context, int userId)
        {
            Console.Clear();
            Console.WriteLine("\n         Creación de nueva solicitud de proyecto           ");
            Console.WriteLine("---------------------------------------------------------\n");

            string titulo = ConsoleInputHelper.ReadString("Título del proyecto: ");
            string descripcion = ConsoleInputHelper.ReadString("Descripción: ");
            int area = ConsoleInputHelper.ReadInt("Área (1-Finanzas, 2-Tecnología, 3-RRHH, 4-Operaciones): ", 1, 4);
            int tipo = ConsoleInputHelper.ReadInt("Tipo (1-Mejora, 2-Innovación, 3-Infrastructure, 4-Capacitación): ", 1, 4);
            decimal monto = ConsoleInputHelper.ReadDecimal("Monto estimado: $");
            int duracion = ConsoleInputHelper.ReadInt("Duración estimada (en días): ", 1);

            Console.WriteLine("\n---------------------------------------------------------\n");

            var service = new ProjectProposalService(
                new ProjectProposalCommand(context),
                new ApprovalRuleQuery(context),
                new ProjectApprovalStepCommand(context)
            );
            var response = await service.CreateProjectProposal(titulo, descripcion, area, tipo, monto, duracion, userId);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nProyecto creado con éxito. ID: {response.Id}");
            Console.ResetColor();
        }
    }
}
