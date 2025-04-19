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

        public ProjectProposalService(IProjectProposalCommand command)
        {
            _command = command;
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

            await _command.CreateProjectProposal(pp);
            return new ProjectProposalResponse
            {
                Id = pp.Id,
                Title = pp.Title,
                Description = pp.Description,
                Area = pp.Area,
                AreaDetail = new GenericResponse
                {
                    Id = pp.AreaDetail.Id,
                    Name = pp.AreaDetail.Name
                },
                Type = pp.Type,
                ProjectType = new GenericResponse
                {
                    Id = pp.ProjectType.Id,
                    Name = pp.ProjectType.Name
                },
                EstimatedAmount = pp.EstimatedAmount,
                EstimatedDuration = pp.EstimatedDuration,
                Status = pp.Status,
                ApprovalStatus = new GenericResponse
                {
                    Id = pp.ApprovalStatus.Id,
                    Name = pp.ApprovalStatus.Name
                },
                CreateAt = pp.CreateAt,
                CreatedBy = pp.CreatedBy,
                CreatedByUser = new UserResponse
                {
                    Id = pp.CreatedByUser.Id,
                    Name = pp.CreatedByUser.Name,
                    Email = pp.CreatedByUser.Email,
                    Role = pp.CreatedByUser.Role,
                    ApproverRole = new GenericResponse
                    {
                        Id = pp.CreatedByUser.ApproverRole.Id,
                        Name = pp.CreatedByUser.ApproverRole.Name
                    }
                },
            };
        }
    }
}