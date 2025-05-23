using Domain.Entities;

namespace Application.Response
{
    public class ProjectShortResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string Area { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

    public class ProjectProposalResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal EstimatedAmount { get; set; }        
        public int EstimatedDuration { get; set; }        
        public required GenericResponse Area { get; set; }
        public required GenericResponse Status { get; set; }        
        public required GenericResponse Type { get; set; }
        public required UserResponse User { get; set; }
    }
    public class ProjectProposalResponseDetail
    {
        public ProjectProposalResponse ProjectProposal { get; set; } = null!;
        public ICollection<ShortApprovalStepResponse> Steps { get; set; } = new List<ShortApprovalStepResponse>();
        public static ProjectProposalResponseDetail Conflict => new ProjectProposalResponseDetail
        {
            ProjectProposal = null!,
            Steps = []
        };
    }
}
