using Application.Exceptions;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Examples
{
    public class ApiErrorExample : IExamplesProvider<ApiError>
    {
        public ApiError GetExamples()
        {
            return new ApiError
            {
                message = "Parámetro de consulta inválido"
            };
        }
    }
}