﻿namespace Domain.Entities
{
    public class ApprovalStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProjectProposal> ProjectProposals { get; set; } = [];
        public ICollection<ProjectApprovalStep> ProjectApprovalSteps { get; set; } = [];
    }
}
