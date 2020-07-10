using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class UserWorkExperience
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EmploymentType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
