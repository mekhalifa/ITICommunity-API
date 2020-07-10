using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITICommunity.Models
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> LogIn(string emailAddress, string password);
        Task<bool> UserExists(string emailAddress);


    }
}
