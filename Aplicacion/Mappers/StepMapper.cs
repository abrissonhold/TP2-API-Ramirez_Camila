using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public static class StepMapper
    {
        public static ApprovalStepResponse ToShortResponse(ProjectApprovalStep step)
        {
            return new ApprovalStepResponse
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
        public static List<ApprovalStepResponse> ToShortResponseList(List<ProjectApprovalStep> steps)
        {
            return steps.Select(ToShortResponse).ToList();
        }

    }
}