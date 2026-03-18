using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Dtos.Project
{
    public class ProjectResponseDetailDto : ProjectResponseDto
    {
        public string OwnerUserId { get; set; } = default!;
    }
}
