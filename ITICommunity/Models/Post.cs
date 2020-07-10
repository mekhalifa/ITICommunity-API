using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Like = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string PostBody { get; set; }
        public string Picture { get; set; }
        public DateTime? Date { get; set; }
        public string Video { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Like> Like { get; set; }
    }
}
