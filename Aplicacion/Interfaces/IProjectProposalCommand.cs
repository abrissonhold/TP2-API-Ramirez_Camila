using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectProposalCommand
    {
        Task<ProjectProposal> CreateProjectProposal(ProjectProposal projectProposal);
        Task UpdateProjectProposal(ProjectProposal projectProposal);
        Task UpdateProjectProposalStatus(ProjectProposal projectProposal);
    }
}
