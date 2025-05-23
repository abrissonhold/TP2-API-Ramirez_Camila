using Application.Response;
using Application.UserCase;
using Domain.Entities;

namespace Application.Mappers
{
    public static class StepMapper
    {
        public static ShortApprovalStepResponse ToShortResponse(ProjectApprovalStep step)
        {
            return new ShortApprovalStepResponse
            {
                Id = step.Id,
                StepOrder = step.StepOrder,
                DecisionDate = step.DecisionDate,
                Observations = step.Observations,
                ApproverRole = GenericMapper.ToResponse(step.ApproverRole),
                Status = GenericMapper.ToResponse(step.ApprovalStatus),                
                ApproverUser = UserMapper.ToResponse(step.ApproverUser),
            };
        }
        public static List<ShortApprovalStepResponse> ToShortResponseList(List<ProjectApprovalStep> steps)
        {
            return steps.Select(s => ToShortResponse(s)).ToList();
        }

    }
}