using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectProposalQuery
    {
        Task<List<ProjectProposal>> GetByFilters(string? title, int? status, int? createdBy, int? approverUser);
        Task<List<ProjectProposal>> GetByCreatorId(int userId);
        Task<ProjectProposal> GetById(Guid id);
        bool ExistsByTitle(string title, Guid? excludeId);
    }
}
