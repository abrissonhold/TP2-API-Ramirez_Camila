namespace Domain.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ProjectProposal> ProjectProposals { get; set; } = [];
        public ICollection<ApprovalRule> ApprovalRules { get; set; } = [];

    }
}
