namespace Domain.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProjectProposal> ProjectProposals { get; set; } = [];
        public ICollection<ApprovalRule> ApprovalRules { get; set; } = [];
    }
}
