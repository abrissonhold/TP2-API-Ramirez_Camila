using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class ApprovalStatusExample : IExamplesProvider<List<GenericResponse>>
    {
        public List<GenericResponse> GetExamples()
        {
            return new List<GenericResponse>
            {
                new() { Id = 1, Name = "Pendiente" },
                new() { Id = 2, Name = "Aprobado" },
                new() { Id = 3, Name = "Rechazado" },
                new() { Id = 4, Name = "En Revisión" }
            };
        }
    }
}
