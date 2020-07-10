using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Track
    {
        public Track()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Track1 { get; set; }
        public int? IntakeId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Intake Intake { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
