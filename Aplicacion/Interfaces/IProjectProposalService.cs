using Application.Response;

namespace Application.Interfaces
{
    public interface IProjectProposalService
    {
        public Task<ProjectProposalResponseDetail> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy);
        public Task<List<ProjectShortResponse>> Search(string? title, int? status, int? applicant, int? approverUser);
        public List<ProjectProposalResponseDetail> GetDetailByUserId(int userId);
        public Task<ProjectProposalResponseDetail> GetById(Guid id);
        public bool ExistingProject(string title);
    }
}
