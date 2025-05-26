﻿namespace Application.Request
{
    public class ProjectProposalRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int Area { get; set; }
        public int Type { get; set; }
        public int CreatedBy { get; set; }
    }
    public class ProjectCreateRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public int Area { get; set; }
        public int Type { get; set; }
        public int User { get; set; }
    }
}
