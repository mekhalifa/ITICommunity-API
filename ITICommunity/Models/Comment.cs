using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Like = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Like> Like { get; set; }
    }
}
