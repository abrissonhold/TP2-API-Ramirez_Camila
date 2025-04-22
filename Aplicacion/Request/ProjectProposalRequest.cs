namespace Application.Request
{
    public class ProjectProposalRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Area { get; set; }
        public int Type { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int CreatedBy { get; set; }
    }
}
