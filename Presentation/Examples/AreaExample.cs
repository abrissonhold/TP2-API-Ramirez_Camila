using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples 
{
    public class AreaExample : IExamplesProvider<List<GenericResponse>>
    {
        public List<GenericResponse> GetExamples()
        {
            return new List<GenericResponse>
        {
            new() { Id = 1, Name = "Finanzas" },
            new() { Id = 2, Name = "Tecnología" },
            new() { Id = 3, Name = "Recursos Humanos" }
        };
        }
    }
}
