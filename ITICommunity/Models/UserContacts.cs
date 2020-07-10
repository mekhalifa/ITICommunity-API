using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class UserContacts
    {
        public int Id { get; set; }
        public string ContactDetails { get; set; }
        public string Description { get; set; }
        public int? ContactTypeId { get; set; }
        public int? UserId { get; set; }
        public bool? Hide { get; set; }

        public virtual ContactType ContactType { get; set; }
        public virtual User User { get; set; }
    }
}
