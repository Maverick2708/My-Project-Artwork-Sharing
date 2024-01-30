using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            People = new HashSet<Person>();
        }

        public int Role { get; set; }
        public string? Permission1 { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
