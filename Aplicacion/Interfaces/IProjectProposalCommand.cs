using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectProposalCommand
    {
        public Task<ProjectProposal> CreateProjectProposal(ProjectProposal projectProposal);
        public Task UpdateProjectProposal(ProjectProposal projectProposal);
        public Task UpdateProjectProposalStatus(ProjectProposal projectProposal);
    }
}
