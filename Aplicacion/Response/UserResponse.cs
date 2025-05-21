using Domain.Entities;

namespace Application.Response
{
    public class UserResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required int Role { get; set; }
        public required GenericResponse ApproverRole { get; set; }
    }
}
