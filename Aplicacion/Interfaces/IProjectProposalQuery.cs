using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectProposalQuery
    {
        List<ProjectProposal> GetByCreatorId(int userId);
    }
}
