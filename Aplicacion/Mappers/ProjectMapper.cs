using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectShortResponse ToShortResponse(ProjectProposal p)
        {
            return new ProjectShortResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Amount = p.EstimatedAmount,
                Duration = p.EstimatedDuration,
                Area = p.AreaDetail.Name,
                Status = p.ApprovalStatus.Name,
                Type = p.ProjectType.Name
            };
        }
        public static List<ProjectShortResponse> ToShortResponseList(List<ProjectProposal> projects)
        {
            return projects.Select(ToShortResponse).ToList();
        }

        public static ProjectProposalResponse ToResponse(ProjectProposal p)
        {
            return new ProjectProposalResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Area = GenericMapper.ToResponse(p.AreaDetail),
                Type = GenericMapper.ToResponse(p.ProjectType),
                EstimatedAmount = p.EstimatedAmount,
                EstimatedDuration = p.EstimatedDuration,
                Status = GenericMapper.ToResponse(p.ApprovalStatus),
                User = UserMapper.ToResponse(p.CreatedByUser),
            };
        }
        public static List<ProjectProposalResponse> ToResponseList(List<ProjectProposal> projects)
        {
            return projects.Select(ToResponse).ToList();
        }

        public static ProjectProposalResponseDetail ToDetailResponse(ProjectProposal p)
        {
            return new ProjectProposalResponseDetail
            {
                ProjectProposal = ToResponse(p),
                Steps = p.ProjectApprovalSteps
                    .Select(StepMapper.ToShortResponse)
                    .OrderBy(s => s.StepOrder).ToList()
            };
        }
        public static List<ProjectProposalResponseDetail> ToDetailResponseList(List<ProjectProposal> projects)
        {
            return projects.Select(ToDetailResponse).ToList();
        }
    }
}