using Application.Interfaces;
using Application.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.UserCase
{
    public class ProjectProposalService : IProjectProposalService
    {
        private readonly IProjectProposalCommand _command;
        private readonly IProjectProposalQuery _query;
        private readonly IApprovalRuleQuery _ruleQuery;
        private readonly IProjectApprovalStepCommand _stepCommand;

        public ProjectProposalService(
            IProjectProposalCommand command, 
            IProjectProposalQuery query, 
            IApprovalRuleQuery ruleQuery, 
            IProjectApprovalStepCommand stepCommand)
        {
            _command = command;
            _query = query;
            _ruleQuery = ruleQuery;
            _stepCommand = stepCommand;
        }

        public async Task<ProjectProposalResponse> CreateProjectProposal(string title, string description,
            int area, int type, decimal estimatedAmount, int estimatedDuration, int createdBy)
        {
            ProjectProposal pp = new ProjectProposal
            {
                Title = title,
                Description = description,
                Area = area,
                Type = type,
                EstimatedAmount = estimatedAmount,
                EstimatedDuration = estimatedDuration,
                Status = 1,
                CreateAt = DateTime.Now,
                CreatedBy = createdBy
            };

            pp = await _command.CreateProjectProposal(pp);

            List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(pp);
            await _stepCommand.CreateProjectApprovalStep(pp, rules);

            return ProjectMapper.ToResponse(pp);
        }

        public List<ProjectProposalResponseDetail> GetDetail(int userId)
        {
            var propuestas = _query.GetByCreatorId(userId);
            return ProjectMapper.ToDetailResponseList(propuestas);
        }
    }
}