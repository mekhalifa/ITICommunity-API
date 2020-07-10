using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Intake
    {
        public Intake()
        {
            Track = new HashSet<Track>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Intake1 { get; set; }

        public virtual ICollection<Track> Track { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
