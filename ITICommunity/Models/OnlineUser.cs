using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class OnlineUser
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? IsOnline { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
