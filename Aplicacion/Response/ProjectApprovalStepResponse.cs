namespace Application.Response
{
    public class ProjectApprovalStepResponse
    {
        public long Id { get; set; }
        public Guid ProjectProposalId { get; set; }
        public ProjectProposalResponse ProjectProposal { get; set; } = null!;
        public int? ApproverUserId { get; set; }
        public UserResponse? ApproverUser { get; set; }
        public int ApproverRoleId { get; set; }
        public GenericResponse ApproverRole { get; set; } = null!;
        public int Status { get; set; }
        public GenericResponse ApprovalStatus { get; set; } = null!;
        public int StepOrder { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string? Observations { get; set; }
    }
    public class ShortApprovalStepResponse
    {
        public long Id { get; set; }
        public int StepOrder { get; set; }        
        public DateTime? DecisionDate { get; set; }        
        public string? Observations { get; set; }        
        public UserResponse? ApproverUser { get; set; }
        public GenericResponse ApproverRole { get; set; } = null!;
        public GenericResponse Status { get; set; } = null!;
    }
}
