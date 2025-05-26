using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class RoleExample : IExamplesProvider<List<GenericResponse>>
    {
        public List<GenericResponse> GetExamples()
        {
            return
            [
                new() { Id = 1, Name = "Administrador" },
                new() { Id = 2, Name = "Gerente" },
                new() { Id = 3, Name = "Analista" }
            ];
        }
    }
}
