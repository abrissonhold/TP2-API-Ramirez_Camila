using Application.Response;

namespace Application.Interfaces
{
    public interface IProjectProposalService
    {
        public Task<ProjectProposalResponse> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy);
    }
}
