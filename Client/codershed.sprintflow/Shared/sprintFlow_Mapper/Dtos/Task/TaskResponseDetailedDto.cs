using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Dtos.Task
{
    public sealed class TaskResponseDetailedDto : TaskResponseDto
    {
        public string ProjectId { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string CreatedByUserId { get; set; } = default!;
        public DateTimeOffset? CreatedUtc { get; set; }
        public DateTimeOffset? CompletedUtc { get; set; }
    }
}
