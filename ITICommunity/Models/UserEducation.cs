using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class UserEducation
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
