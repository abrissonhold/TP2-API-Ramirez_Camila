namespace Domain.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProjectProposal> ProjectProposals { get; set; } = new List<ProjectProposal>();
        public ICollection<ApprovalRule> ApprovalRules { get; set; } = new List<ApprovalRule>();
    }
}
