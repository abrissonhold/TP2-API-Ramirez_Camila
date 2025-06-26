using System.Text.Json.Serialization;

namespace Application.Exceptions
{
    public class ApiError
    {
        public string message { get; set; } = string.Empty;
    }

}