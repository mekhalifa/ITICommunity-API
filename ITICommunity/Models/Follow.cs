using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Follow
    {
        public int UserId { get; set; }
        public int FollowingId { get; set; }

        public virtual User Following { get; set; }
        public virtual User User { get; set; }
    }
}
