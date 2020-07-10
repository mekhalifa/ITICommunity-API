using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITICommunity.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string name { get; set; }

        [EmailAddress]
        [Required]
        public string emailAddress { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }


        public string nationalId { get; set; }

        public int? intakeId { get; set; }

        public int? branchId { get; set; }
        public int? trackId { get; set; }

    }
}
