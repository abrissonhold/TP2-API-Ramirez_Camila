using Application.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class ProjectCreateRequestExample : IExamplesProvider<ProjectCreateRequest>
    {
        public ProjectCreateRequest GetExamples()
        {
            return new ProjectCreateRequest
            {
                Title = "Sistema de Control de Acceso",
                Description = "Implementación de un sistema biométrico para el control de acceso al edificio",
                Amount = 75000,
                Duration = 120,
                Area = 2,
                Type = 1,
                User = 1
            };
        }
    }
}
