using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Dtos.Project
{
    public class ProjectUpdateRequestDto
    {
       public string Id { get; set; } = default!;
       public string Name { get; set; } = default!;
       public string Description { get; set; } = "";
       
    }
}
