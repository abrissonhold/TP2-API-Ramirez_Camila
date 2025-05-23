namespace Application.Request
{
    public class ProjectUpdateRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
    }
}