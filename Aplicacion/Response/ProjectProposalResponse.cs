using Domain.Entities;

namespace Application.Response
{
    public class ProjectProposalResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Area { get; set; }
        public required GenericResponse AreaDetail { get; set; }
        public int Type { get; set; }
        public required GenericResponse ProjectType { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int Status { get; set; }
        public required GenericResponse ApprovalStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreatedBy { get; set; }
        public required UserResponse CreatedByUser { get; set; }
    }
    public class ProjectProposalResponseDetail
    {
        public ProjectProposalResponse ProjectProposal { get; set; } = null!;
        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; } = new List<ProjectApprovalStep>();
    }
}
