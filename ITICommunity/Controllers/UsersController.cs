using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITICommunity.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ITICommunity.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITICommunityContext _context;
        private readonly IMapper _mapper;
        private string imgFolder = "/Images/Upload/";
        private string defaultAvatar = "profile2.png";
        public UsersController(ITICommunityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;        
        }

        // GET: api/Users
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            //getting only the active user approved by admin
            var users = _context.Users.Where(x => x.IsActive==true); 
            return await users.ToListAsync();
        }

        // GET: api/Users/new
        [HttpGet("new")]
        public async Task<ActionResult<IEnumerable<User>>> GetNewUser()
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            var newUsers = from M in _context.Users
                           where M.ApprovementDate >= dateCriteria
                           where M.IsActive == true
                           group M by M.Id into G
                           select new
                           {
                               MerchantId = G.Select(m => m.Id)
                           };
            var users = _context.Users.Where(x => x.IsActive == true).Where(x => x.ApprovementDate >= dateCriteria);
            return await users.ToListAsync();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public dynamic GetUser(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).Where(x=>x.IsActive==true);

            if (user == null )
            {
                return NotFound();
            }
            var customUser = (from u in _context.Users.Include(x=>x.Branch).Include(x=>x.Track).ToList()
                              where u.IsActive == true
                              where u.Id == id
                              select new {
                                  // about=u.About,
                                  bgPic = u.BgPic,
                                  track1 = u.Track.Track1,
                                  branch = u.Branch.BranchLocation,
                              name = u.Name,
                                  jobTitle = u.JobTitle,
                                  id = u.Id,
                                  //ProfilePic= imgFolder + (String.IsNullOrEmpty(u.ProfilePic) ? defaultAvatar : u.Id + "." + u.Id),
                                  profilePic = u.ProfilePic,
                                  birthDate = u.BirthDate,
                                  ///get for Auth.
                                  userName = u.UserName,
                                  normalizedUserName = u.NormalizedUserName,
                                  email = u.Email,
                                  normalizedEmail = u.NormalizedEmail,
                                  emailConfirmed = u.EmailConfirmed,
                                  passwordHash = u.PasswordHash,
                                  securityStamp = u.SecurityStamp,
                                  concurrencyStamp = u.ConcurrencyStamp,
                                  phoneNumber = u.PhoneNumber,
                                  phoneNumberConfirmed = u.PhoneNumberConfirmed,
                                  twoFactorEnabled = u.TwoFactorEnabled,
                                  lockoutEnd = u.LockoutEnd,
                                  lockoutEnabled = u.LockoutEnabled,
                                  accessFailedCount = u.AccessFailedCount



                                  // cvfile=u.Cvfile,
                                  //email=u.Email,
                                  //otheremails = u.UserContacts.Where(x => x.Id == 2),
                                  //phones =u.UserContacts.Where(x=>x.Id==1),
                                  //websites=u.UserContacts.Where(x=>x.Id==2),

                              }).ToList();
            return customUser;
        }

        // GET: api/Users/Following/1
        [HttpGet("Following{followingid}")]
        public dynamic GetUserFollowing(int followingid)
        {
            var user = _context.Users.Where(x => x.IsActive == true);

            if (user == null)
            {
                return NotFound();
            }
            var customUser = (from u in _context.Users.ToList()
                              join f in _context.Follows.ToList() on u.Id equals f.UserId
                              where f.UserId ==followingid
                              where u.IsActive == true
                             // where u.Id == id
                              select new
                              {
                                   about=u.About,
                                  bgPic = f.User.BgPic,
                                  name = f.User.Name,
                                  jobTitle = f.User.JobTitle,
                                  id = f.User.Id,
                                  //ProfilePic= imgFolder + (String.IsNullOrEmpty(u.ProfilePic) ? defaultAvatar : u.Id + "." + u.Id),
                                  profilePic = f.User.ProfilePic,
                                  birthDate = f.User.BirthDate,
                                  following=f.FollowingId
                                  // cvfile=u.Cvfile,
                                  //email=u.Email,
                                  //otheremails = u.UserContacts.Where(x => x.Id == 2),
                                  //phones =u.UserContacts.Where(x=>x.Id==1),
                                  //websites=u.UserContacts.Where(x=>x.Id==2),

                              }).ToList();
            return customUser;
        }

        // GET: api/Users/work/5
        [HttpGet("work/{userid}")]
        public dynamic GetUserWorkExperience(int userid)
        {
            var user = _context.Users.Where(x => x.IsActive == true).Where(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            var userwork = _context.UserWorkExperiences.Where(x => x.UserId == userid).ToList();

            return userwork;
        }

        // GET: api/Users/story/5
        [HttpGet("story/{userid}")]
        public dynamic GetUserstory(int userid)
        {
            var user = _context.Users.Where(x => x.IsActive == true).Where(x => x.Id == userid).Select(x => x.ITIStory);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // GET: api/Users/education/5
        [HttpGet("education/{userid}")]
        public dynamic GetUserEducation(int userid)
        {
            var user = _context.Users.Where(x => x.IsActive == true).Where(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            var useredu = _context.UserEducations.Where(x => x.UserId == userid).ToList();

            return useredu;
        }

        // GET: api/Users/contacts/5
        [HttpGet("contacts/{userid}")]
        public dynamic GetUserContacts(int userid)
        {
            var user = _context.Users.Where(x => x.IsActive == true).Where(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            // var usercont = _context.UserContacts.Where(x => x.UserId == userid).ToList();
            var usercontactnew = (from c in _context.UserContacts.ToList()
                                  join ct in _context.ContactTypes.ToList() on c.ContactTypeId equals ct.Id
                                  where c.UserId == userid
                                  select new
                                  {
                                      contactDetails = c.ContactDetails,
                                      type = ct.Type,
                                      description = c.Description,
                                      id = c.Id
                                  }
                                ).ToList();
            return usercontactnew;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// //usable when admin approve user,edit profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);//hyrg3 getall bl user l gded
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }


        [HttpGet("search/{name}")]
        public dynamic GetUserByName(string name)
        {
            var users = _context.Users.Where(x => x.Name == name).ToList();

            if (users.Count == 0)
            {
                return NotFound();
            }

            return users;
        }


        /// /Delete: api/User/deletestory/5
        [HttpDelete("deletestory/{storyuserid}")]
        public async Task<ActionResult<User>> DeleteUserStory(int storyuserid)
        {
            var user = await _context.Users.FindAsync(storyuserid);
            if (user == null)
            {
                return NotFound();
            }

            user.ITIStory = "";
            await _context.SaveChangesAsync();

            return user;
        }

        // post:api/User/addstory/5
        [HttpPost("addstory/{id}/{story}")]
        public async Task<ActionResult<User>> PostUserStory(int id, string story)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.ITIStory = story;
            await _context.SaveChangesAsync();

            return user;
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
