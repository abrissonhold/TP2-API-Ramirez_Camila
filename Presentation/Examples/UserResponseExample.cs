using Application.Response;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class UserResponseExample : IExamplesProvider<List<UserResponse>>
    {
        public List<UserResponse> GetExamples()
        {
            return new List<UserResponse>
            {
                new()
                {
                    Id = 1,
                    Name = "Juan Pérez",
                    Email = "juan.perez@empresa.com",
                    Role = 2,
                    ApproverRole = new GenericResponse
                    {
                        Id = 2,
                        Name = "Gerente"
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Ana González",
                    Email = "ana.gonzalez@empresa.com",
                    Role = 1,
                    ApproverRole = new GenericResponse
                    {
                        Id = 3,
                        Name = "Analista"
                    }
                }
            };
        }
    }
}
