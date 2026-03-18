using System;
using System.Collections.Generic;
using System.Text;

namespace sprintFlow_Mapper.Models.Project
{
    public class Project
    {

        public string Id { get; set; } = default!;
        public string OrgId { get; set; } = default!; //partition key
        public const string type = "project";
        public string Name { get; set; } = default!;
        public string Description { get; set; } = "";
        public string OwnerUserId { get; set; } = default!;
        public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;

    }
}
