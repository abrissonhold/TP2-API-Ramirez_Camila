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
                Message = "Parámetro de consulta inválido"
            };
        }
    }
}