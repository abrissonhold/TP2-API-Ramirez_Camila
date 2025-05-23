using Application.Request;
using Swashbuckle.AspNetCore.Filters;

public class ProjectUpdateExample : IExamplesProvider<ProjectUpdateRequest>
{
    public ProjectUpdateRequest GetExamples()
    {
        return new ProjectUpdateRequest
        {
            Title = "Sistema de Gestión de Inventarios y Logística",
            Description = "Desarrollo de un sistema integral para administrar el inventario y la logística de la empresa",
            Duration = 120
        };
    }
}
