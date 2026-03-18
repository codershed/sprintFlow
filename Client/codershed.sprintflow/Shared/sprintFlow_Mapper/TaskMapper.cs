using sprintFlow_Mapper.Dtos.Task;
using sprintFlow_Mapper.Models.Task;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace sprintFlow_Mapper
{
    public class TaskMapper
    {
        public static TaskItem ToEntity(TaskCreateRequestDto dto)
        {
            if (dto == null) return null;
            

            return new TaskItem
            {
                
                Title = dto.Title,
                Status = dto.Status,
                Priority = dto.Priority,
                AssignedToUserId = dto.AssignedToUserId,
                DueUtc = dto.DueUtc,
                ProjectId = dto.ProjectId,
                Description = dto.Description,
                CreatedUtc = DateTime.UtcNow
            };
        }
        public static TaskItem ToEntity(TaskUpdateRequestDto dto)
        {
            if (dto == null) return null;

            return new TaskItem
            {
                Id = dto.Id,
                Title = dto.Title,
                Status = dto.Status,
                Priority = dto.Priority,
                AssignedToUserId = dto.AssignedToUserId,
                DueUtc = dto.DueUtc,
                ProjectId = dto.ProjectId,
                Description = dto.Description,
                CreatedUtc = DateTime.UtcNow
            };
        }
        public static TaskResponseDto ToResponseDto(TaskItem task)
        {
            if (task == null) return null;

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                Priority = task.Priority,
                AssignedToUserId = task.AssignedToUserId,
                DueUtc = task.DueUtc
            };
        }

        public static TaskResponseDetailedDto ToResponseDetailedDto(TaskItem task)
        {
            if (task == null) return null;

            return new TaskResponseDetailedDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                Priority = task.Priority,
                AssignedToUserId = task.AssignedToUserId,
                DueUtc = task.DueUtc,
                ProjectId = task.ProjectId,
                Description = task.Description,
                CreatedByUserId = task.CreatedByUserId,
                CreatedUtc = task.CreatedUtc,
                CompletedUtc = task.CompletedUtc
            };
        }
    }
}
