using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class ProjectShortResponseExample : IExamplesProvider<List<ProjectShortResponse>>
    {
        public List<ProjectShortResponse> GetExamples()
        {
            return
            [
                new ProjectShortResponse
                {
                    Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                    Title = "Sistema de Gestión de Inventarios",
                    Description = "Desarrollo de un sistema para administrar el inventario de la empresa",
                    Amount = 50000,
                    Duration = 90,
                    Area = "Tecnología",
                    Status = "Pendiente",
                    Type = "Desarrollo"
                },
                new ProjectShortResponse
                {
                    Id = Guid.Parse("223e4567-e89b-12d3-a456-426614174001"),
                    Title = "Mejora de Procesos Contables",
                    Description = "Optimización de los procesos contables actuales",
                    Amount = 25000,
                    Duration = 45,
                    Area = "Finanzas",
                    Status = "Aprobado",
                    Type = "Mejora de Procesos"
                }
            ];
        }
    }
}