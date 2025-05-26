using Application.Response;
using Swashbuckle.AspNetCore.Filters;

public class ProjectResponseExample : IExamplesProvider<ProjectProposalResponseDetail>
{
    public ProjectProposalResponseDetail GetExamples()
    {
        return new ProjectProposalResponseDetail
        {
            ProjectProposal = new ProjectProposalResponse
            {
                Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                Title = "Sistema de Gestión de Inventarios y Logística",
                Description = "Desarrollo de un sistema integral para administrar el inventario y la logística de la empresa",
                EstimatedAmount = 50000,
                EstimatedDuration = 120,
                Area = new GenericResponse { Id = 2, Name = "Tecnología" },
                Status = new GenericResponse { Id = 1, Name = "Pendiente" },
                Type = new GenericResponse { Id = 1, Name = "Desarrollo" },
                User = new UserResponse
                {
                    Id = 1,
                    Name = "Juan Pérez",
                    Email = "juan.perez@empresa.com",
                    Role = 2,
                    ApproverRole = new GenericResponse { Id = 2, Name = "Gerente" }
                }
            },
            Steps =
            [
                new ShortApprovalStepResponse
                {
                    Id = 1,
                    StepOrder = 1,
                    DecisionDate = null,
                    Observations = null,
                    ApproverUser = new UserResponse
                    {
                        Id = 1,
                        Name = "Juan Pérez",
                        Email = "juan.perez@empresa.com",
                        Role = 2,
                        ApproverRole = new GenericResponse { Id = 2, Name = "Gerente" }
                    },
                    ApproverRole = new GenericResponse { Id = 2, Name = "Gerente" },
                    Status = new GenericResponse { Id = 1, Name = "Pendiente" }
                }
            ]
        };
    }
}
