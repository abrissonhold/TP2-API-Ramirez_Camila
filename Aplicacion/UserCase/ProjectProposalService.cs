using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCase
{
    public class ProjectProposalService : IProjectProposalService
    {
        private readonly IProjectProposalCommand _command;
        private readonly IApprovalRuleQuery _ruleQuery;
        private readonly IProjectApprovalStepCommand _stepCommand;
        public ProjectProposalService(IProjectProposalCommand command, IApprovalRuleQuery ruleQuery, IProjectApprovalStepCommand stepCommand)
        {
            _command = command;
            _ruleQuery = ruleQuery;
            _stepCommand = stepCommand;
        }

        public async Task<ProjectProposalResponse> CreateProjectProposal(ProjectProposalRequest ppr)
        {
            ProjectProposal pp = new ProjectProposal
            {
                Title = ppr.Title,
                Description = ppr.Description,
                Area = ppr.Area,
                Type = ppr.Type,
                EstimatedAmount = ppr.EstimatedAmount,
                EstimatedDuration = ppr.EstimatedDuration,
                Status = 1,
                CreateAt = DateTime.Now,
                CreatedBy = ppr.CreatedBy,
            };

            pp = await _command.CreateProjectProposal(pp);

            List<ApprovalRule> rules = _ruleQuery.GetApplicableRule(pp);
            await _stepCommand.CreateProjectApprovalStep(pp, rules);

            return new ProjectProposalResponse
            {
                Id = pp.Id,
                Title = pp.Title,
                Description = pp.Description,
                Area = pp.Area,
                AreaDetail = new GenericResponse
                {
                    Id = pp.Area,
                    Name = pp.AreaDetail.Name
                },
                Type = pp.Type,
                ProjectType = new GenericResponse
                {
                    Id = pp.Type,
                    Name = pp.ProjectType.Name
                },
                EstimatedAmount = pp.EstimatedAmount,
                EstimatedDuration = pp.EstimatedDuration,
                Status = pp.Status,
                ApprovalStatus = new GenericResponse
                {
                    Id = pp.Status,
                    Name = pp.ApprovalStatus.Name
                },
                CreateAt = pp.CreateAt,
                CreatedBy = pp.CreatedBy,
                CreatedByUser = new UserResponse
                {
                    Id = pp.CreatedBy,
                    Name = pp.CreatedByUser.Name,
                    Email = pp.CreatedByUser.Email,
                    Role = pp.CreatedByUser.Role,
                    ApproverRole = new GenericResponse
                    {
                        Id = pp.CreatedByUser.Role,
                        Name = pp.CreatedByUser.ApproverRole.Name
                    }
                },
            };
        }
    }
}