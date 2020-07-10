using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ITICommunity.DTOs;
using ITICommunity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITICommunityContext itiCommunityContext;

        public AuthController(IConfiguration _configuration, UserManager<User> _userManager,
            SignInManager<User> _signInManager, ITICommunityContext _itiCommunityContext)
        {
            configuration = _configuration;
            userManager = _userManager;
            signInManager = _signInManager;
            itiCommunityContext = _itiCommunityContext;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = new User
            {
                Name = userForRegisterDto.name,
                Email = userForRegisterDto.emailAddress,
                NationalId = userForRegisterDto.nationalId,
                UserName = userForRegisterDto.userName,
                BranchId = userForRegisterDto.branchId,
                IntakeId = userForRegisterDto.intakeId,
                TrackId = userForRegisterDto.trackId,
                IsActive = true,////????
                ApprovementDate= DateTime.UtcNow,
                ProfilePic = "/Images/Upload/profile2.png",
                BgPic= "/Images/Upload/back2.jpg",
                ITIStory=""
            };
            var userToCheckForEmail = itiCommunityContext.Users.FirstOrDefault(x => x.Email == userForRegisterDto.emailAddress);
            if (userToCheckForEmail != null)
                return BadRequest("Email Address Already Exists");

            var userToCheckForUsername = itiCommunityContext.Users.FirstOrDefault(x => x.UserName == userForRegisterDto.userName);
            if (userToCheckForUsername != null)
                return BadRequest("Username Address Already Exists");


            IdentityResult result = await userManager.CreateAsync(userToCreate, userForRegisterDto.password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn (UserForLoginDto userForLoginDto)
        {
            var user = await userManager.FindByEmailAsync(userForLoginDto.emailaddress);
            if (user == null)
                return Unauthorized();
            var result = await signInManager.CheckPasswordSignInAsync(user, userForLoginDto.password, false);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    token = GenerateJwtToken(user),
                    user
                });
            }

            return Unauthorized();



            

        }

        private string GenerateJwtToken(User userFromRepo)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim (ClaimTypes.Email, userFromRepo.Email)
 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credintials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}