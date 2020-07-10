using System.ComponentModel.DataAnnotations;

namespace ITICommunity.Controllers
{
    public class UserForLoginDto
    {
        
        [EmailAddress]
        [Required]
        public string emailaddress { get; set; }
        [Required]
        public string password { get; set; }

    }
}