using Application.Response;

namespace Application.Interfaces
{
    public interface IProjectProposalService
    {
        Task<ProjectProposalResponseDetail> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy);
        Task<List<ProjectShortResponse>> Search(string? title, int? status, int? applicant, int? approverUser);
        List<ProjectProposalResponseDetail> GetDetailByUserId(int userId);
        Task<ProjectProposalResponseDetail> GetById(Guid id);
        bool ExistingProject(string title, Guid? excludeId);
        Task<ProjectProposalResponseDetail> ProcessDecision(Guid projectId, int stepId, int userId, int status, string? observation);
        Task<ProjectProposalResponseDetail?> UpdateProject(Guid id, string title, string description, int duration);

    }
}
