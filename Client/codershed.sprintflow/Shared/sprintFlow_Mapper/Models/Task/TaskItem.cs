using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Models.Task
{
    public class TaskItem
    {
        public string Id { get; set; } = default!;          
        public const string Type = "task";          
        public string OrgId { get; set; } = default!;       
        public string ProjectId { get; set; } = default!;   
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; } = "New";         
        public string Priority { get; set; } = "Normal";    
        public string CreatedByUserId { get; set; } = default!;
        public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;
        public string? AssignedToUserId { get; set; }
        public DateTimeOffset? DueUtc { get; set; }
        public DateTimeOffset? CompletedUtc { get; set; }
        public List<string> Labels { get; set; } = new();
    }
}
