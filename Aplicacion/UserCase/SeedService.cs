using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCase
{
    public class SeedService
    {
        private readonly IProjectProposalCommand _projectCommand;
        private readonly IProjectApprovalStepCommand _stepCommand;
        private readonly IApprovalRuleQuery _ruleQuery;

        public SeedService(
            IProjectProposalCommand projectCommand,
            IProjectApprovalStepCommand stepCommand,
            IApprovalRuleQuery ruleQuery)
        {
            _projectCommand = projectCommand;
            _stepCommand = stepCommand;
            _ruleQuery = ruleQuery;
        }

        public async Task SeedProyectosAsync()
        {
            var titulos = new[] { "Sistema de Accesos", "Nueva Sucursal", "Plataforma E-learning", "Reforma Edilicia", "Compra de equipos" };
            var descripciones = new[] { "Proyecto para modernizar el sistema", "Expansión regional", "Capacitación digital", "Mejoras de infraestructura", "Adquisición tecnológica" };
            var areas = new[] { 1, 2, 3, 4 };
            var tipos = new[] { 1, 2, 3, 4 };
            var usuarios = new[] { 1, 2, 3, 4, 5, 6 };      

            var random = new Random();

            for (int i = 0; i < 20; i++)
            {
                var titulo = titulos[random.Next(titulos.Length)] + " #" + (i + 1);
                var descripcion = descripciones[random.Next(descripciones.Length)];
                var area = areas[random.Next(areas.Length)];
                var tipo = tipos[random.Next(tipos.Length)];
                var monto = random.Next(10000, 1000000);
                var duracion = random.Next(5, 90);
                var usuario = usuarios[random.Next(usuarios.Length)];

                ProjectProposal pp = new()
                {
                    Title = titulo,
                    Description = descripcion,
                    Area = area,
                    Type = tipo,
                    EstimatedAmount = monto,
                    EstimatedDuration = duracion,
                    Status = 1,
                    CreateAt = DateTime.Now,
                    CreatedBy = usuario
                };
                _ = await _projectCommand.CreateProjectProposal(pp);

                List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(pp);
                await _stepCommand.CreateProjectApprovalStep(pp, rules);
            }
        }
    }

}
