using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            UserContacts = new HashSet<UserContacts>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<UserContacts> UserContacts { get; set; }
    }
}
