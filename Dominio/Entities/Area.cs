namespace Domain.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ProjectProposal> ProjectProposals { get; set; } = new List<ProjectProposal>();
        public ICollection<ApprovalRule> ApprovalRules { get; set; } = new List<ApprovalRule>();

    }
}
