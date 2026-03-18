using sprintFlow_Mapper.Dtos.Project;
using sprintFlow_Mapper.Models.Project;

namespace sprintFlow_Mapper
{
    public class ProjectMapper
    {
        public static Project ToEntity(ProjectCreateRequestDto dto)
        {
            if (dto == null) return null;

            var project = new Project();
            // Map properties from dto to project entity
            project.Name = dto.Name;
            project.Description = dto.Description;

            return project;
        }
        public static Project ToEntity(ProjectUpdateRequestDto dto)
        {
            if (dto == null) return null;

            var project = new Project();
            
            project.Id = dto.Id;
            project.Name = dto.Name;
            project.Description = dto.Description;

            return project;
        }

        public static ProjectResponseDto ToResponseDto(Project project)
        {
            if (project == null) return null;

            var responseDto = new ProjectResponseDto();
            
            responseDto.Id = project.Id;
            responseDto.Name = project.Name;
            responseDto.Description = project.Description;
            responseDto.CreatedUtc = project.CreatedUtc;

            return responseDto;
        }
         public static ProjectResponseDetailDto ToResponseDetailDto(Project project)
        {
            if (project == null) return null;

            var detailDto = new ProjectResponseDetailDto();
            
            detailDto.Id = project.Id;
            detailDto.Name = project.Name;
            detailDto.Description = project.Description;
            detailDto.CreatedUtc = project.CreatedUtc;
            detailDto.OwnerUserId = project.OwnerUserId;

            return detailDto;
        }
    }
}
