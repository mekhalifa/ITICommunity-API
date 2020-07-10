using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Track = new HashSet<Track>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string BranchLocation { get; set; }

        public virtual ICollection<Track> Track { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
