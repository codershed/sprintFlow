using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Dtos.Project
{
    public class ProjectResponseDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = "";
        public DateTimeOffset CreatedUtc { get; set; }
    }

    

}
