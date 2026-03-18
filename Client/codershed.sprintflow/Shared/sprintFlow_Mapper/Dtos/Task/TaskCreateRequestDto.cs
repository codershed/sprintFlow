using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Dtos.Task
{
    public class TaskCreateRequestDto
    {
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? AssignedToUserId { get; set; }
        public DateTimeOffset? DueUtc { get; set; }
        
    }
}
