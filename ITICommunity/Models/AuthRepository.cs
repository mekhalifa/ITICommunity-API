using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace ITICommunity.Models
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ITICommunityContext _context;

        public AuthRepository(ITICommunityContext context)
        {
            _context = context;
        }
        public async Task<User> LogIn(string emailAddress, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == emailAddress);
            if (user == null)
                return null;
            CreateHashPassword(password, out byte[] hashPassword);
            // Identity SingInManager will do the hashing
            //if (user.Password != hashPassword)
            //    return null;
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash;
            CreateHashPassword(password, out passwordHash);

            //user.Password = passwordHash;
            
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string emailAddress)
        {
            if (await _context.Users.AnyAsync(i => i.Email == emailAddress))
                return true;
            return false;

        }
    }
}
