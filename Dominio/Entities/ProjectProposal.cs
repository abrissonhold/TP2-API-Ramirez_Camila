namespace Domain.Entities
{
    public class ProjectProposal
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Area { get; set; }
        public Area AreaDetail { get; set; } = null!;
        public int Type { get; set; }
        public ProjectType ProjectType { get; set; } = null!;
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int Status { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; } = new List<ProjectApprovalStep>();
    }
}
