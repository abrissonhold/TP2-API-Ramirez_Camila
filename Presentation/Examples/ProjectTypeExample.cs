using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class ProjectTypeExample : IExamplesProvider<List<GenericResponse>>
    {
        public List<GenericResponse> GetExamples()
        {
            return new List<GenericResponse>
            {
                new() { Id = 1, Name = "Desarrollo" },
                new() { Id = 2, Name = "Investigación" },
                new() { Id = 3, Name = "Mejora de Procesos" }
            };
        }
    }
}