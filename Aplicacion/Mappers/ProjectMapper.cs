using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectProposalResponse ToResponse(ProjectProposal p)
        {
            return new ProjectProposalResponse
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Area = p.Area,
                AreaDetail = GenericMapper.ToResponse(p.AreaDetail),
                Type = p.Type,
                ProjectType = GenericMapper.ToResponse(p.ProjectType),
                EstimatedAmount = p.EstimatedAmount,
                EstimatedDuration = p.EstimatedDuration,
                Status = p.Status,
                ApprovalStatus = GenericMapper.ToResponse(p.ApprovalStatus),
                CreateAt = p.CreateAt,
                CreatedBy = p.CreatedBy,
                CreatedByUser = UserMapper.ToResponse(p.CreatedByUser)
            };
        }

        public static List<ProjectProposalResponse> ToResponseList(List<ProjectProposal> projects)
        {
            return projects.Select(p => ToResponse(p)).ToList();
        }
        public static ProjectProposalResponseDetail ToDetailResponse(ProjectProposal p)
        {
            return new ProjectProposalResponseDetail
            {
                ProjectProposal = ToResponse(p),
                ProjectApprovalSteps = p.ProjectApprovalSteps.OrderBy(s => s.StepOrder).ToList()
            };
        }
        public static List<ProjectProposalResponseDetail> ToDetailResponseList(List<ProjectProposal> projects)
        {
            return projects.Select(p => ToDetailResponse(p)).ToList();
        }
    }
}