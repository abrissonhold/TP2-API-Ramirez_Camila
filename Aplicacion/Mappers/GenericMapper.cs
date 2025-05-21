using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public static class GenericMapper
    {
        public static GenericResponse ToResponse(Area area)
        {
            return new GenericResponse 
            { 
                Id = area.Id, 
                Name = area.Name 
            };
        }        
        public static List<GenericResponse> ToResponseList(List<Area> areas)
        {
            return areas.Select(ToResponse).ToList();
        }

        public static GenericResponse ToResponse(ProjectType type)
        { 
            return new GenericResponse 
            { 
                Id = type.Id, 
                Name = type.Name 
            };
        }
        public static List<GenericResponse> ToResponseList(List<ProjectType> types)
        {
            return types.Select(ToResponse).ToList();
        }           

        public static GenericResponse ToResponse(ApprovalStatus status)      
        { 
            return new GenericResponse 
            { 
                Id = status.Id, 
                Name = status.Name
            };
        }
        public static List<GenericResponse> ToResponseList(List<ApprovalStatus> statuses)
        {
            return statuses.Select(ToResponse).ToList();
        }

        public static GenericResponse ToResponse(ApproverRole role)
        {
            return new GenericResponse
            {
                Id = role.Id,
                Name = role.Name
            };
        }
        public static List<GenericResponse> ToResponseList(List<ApproverRole> roles)
        {
            return roles.Select(ToResponse).ToList();
        }
    }
}