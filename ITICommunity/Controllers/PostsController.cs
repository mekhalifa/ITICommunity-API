using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITICommunity.Models;
using Microsoft.AspNetCore.Cors;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("MyPolicy")]

    public class PostsController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public PostsController(ITICommunityContext context)
        {
            _context = context;
            //Post p = new Post { UserId = 2, PostBody = "hello this is my 1st post here",Date=Convert.ToDateTime("14/02/2020") };
            //_context.Post.Add(p);
            //_context.SaveChanges();
        }
      //  private string imgFolder = "/Images/Upload/";
        private string defaultAvatar = "/Images/Upload/profile2.png";
        // GET: api/Posts
        [HttpGet]
        public dynamic GetPost()
        {
            var x =_context.Posts.Select(x=>x.User).ToList();
            var postQuer = (from post in _context.Posts.ToList()
                                //join u in _context.User.ToList() on post.UserId equals u.Id
                            orderby post.Date descending
                            select new Post
                            {
                                Date = post.Date,
                                Id = post.Id,
                                UserId = post.UserId,
                                Comment = post.Comment,
                                PostBody = post.PostBody,
                                Picture = post.Picture,
                                Video = post.Video,
                                User = post.User
                            }).ToList();
            var newpost= (from post in _context.Posts.Include(x => x.Like).ToList()
                          orderby post.Date descending
                          select new
                          {
                              Postbody = post.PostBody,
                              PostedBy = post.UserId,
                              PostedByName = post.User.UserName,
                              PostedByAvatar =  (String.IsNullOrEmpty(post.User.ProfilePic) ? defaultAvatar : post.User.ProfilePic),
                              PostedDate = post.Date,
                              PostId = post.Id,
                              PostPhoto=post.Picture,
                              PostLikes=post.Like,
                              PostComments = from comment in post.Comment.ToList()
                                             orderby comment.Date
                                             select new
                                             {
                                                 CommentedBy = comment.UserId,
                                                 CommentedByName = comment.User.UserName,
                                                 CommentedByAvatar =  (String.IsNullOrEmpty(comment.User.ProfilePic) ? defaultAvatar : comment.User.ProfilePic),
                                                 CommentedDate = comment.Date,
                                                 CommentId = comment.Id,
                                                 Message = comment.CommentBody,
                                                 Postid = comment.PostId

                                                               }
                                             }).AsEnumerable();

            return newpost;
        }

        //[HttpPost]
        //public dynamic PostPostNewV(Post post,int userPostedID)
        //{
        //    post.UserId = userPostedID;////????
        //    post.Date = DateTime.UtcNow;
        //    ModelState.Remove("post.UserId");
        //    ModelState.Remove("post.Date");

        //    if (ModelState.IsValid)
        //    {
        //        _context.Post.Add(post);
        //        _context.SaveChanges();
        //        var usr = _context.User.FirstOrDefault(x => x.Id == post.UserId);
        //        var ret = new
        //        {
        //            Message = post.Message,
        //            PostedBy = post.PostedBy,
        //            PostedByName = usr.UserName,
        //            PostedByAvatar = imgFolder + (String.IsNullOrEmpty(usr.AvatarExt) ? defaultAvatar : post.PostedBy + "." + post.UserProfile.AvatarExt),
        //            PostedDate = post.PostedDate,
        //            PostId = post.PostId
        //        };
        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ret);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = post.PostId }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        // GET: api/Posts/5

        [HttpGet("{id}")]
        public dynamic GetPost(int id)
        {
            var postisValid =  _context.Posts.Find(id);

            if (postisValid == null)
            {
                return NotFound();
            }
            var x = _context.Posts.Select(x => x.User).ToList();
            var newpost = (from post in _context.Posts.ToList()
                           where post.Id == id
                           select new
                           {
                               postbody = post.PostBody,
                               PostedBy = post.UserId,
                               PostedByName = post.User.UserName,
                               PostedByAvatar =  (String.IsNullOrEmpty(post.User.ProfilePic) ? defaultAvatar : post.User.ProfilePic),
                               PostedDate = post.Date,
                               PostId = post.Id,
                               PostComments = from comment in post.Comment.ToList()
                                              orderby comment.Date
                                              select new
                                              {
                                                  CommentedBy = comment.UserId,
                                                  CommentedByName = comment.User.UserName,
                                                  CommentedByAvatar =  (String.IsNullOrEmpty(comment.User.ProfilePic) ? defaultAvatar : comment.User.ProfilePic),
                                                  CommentedDate = comment.Date,
                                                  CommentId = comment.Id,
                                                  Message = comment.CommentBody,
                                                  Postid = comment.PostId

                                              },
                               postLikes = post.Like
                           }).AsEnumerable();

            return newpost;
            //return post;
        }


        /// <summary>
        /// Get Posts by userid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpGet("userpost{userid}")]
        public dynamic GetUserPost(int userid)
        {
            
         var postisValid = _context.Posts.Find(userid);
   if (postisValid == null)
            {
                return NotFound();
            }
            var x = _context.Posts.Select(x => x.User).ToList();
            var newpost = (from post in _context.Posts.ToList()
                           where post.UserId == userid
                           orderby post.Date descending
                           select new
                           {
                               postbody = post.PostBody,
                               PostedBy = post.UserId,
                               PostedByName = post.User.UserName,
                               PostedByAvatar =  (String.IsNullOrEmpty(post.User.ProfilePic) ? defaultAvatar : post.User.ProfilePic),
                               PostedDate = post.Date,
                               PostId = post.Id,
                               PostPhoto = post.Picture,
                               PostComments = from comment in post.Comment.ToList()
                                              orderby comment.Date
                                              select new
                                              {
                                                  CommentedBy = comment.UserId,
                                                  CommentedByName = comment.User.UserName,
                                                  CommentedByAvatar =  (String.IsNullOrEmpty(comment.User.ProfilePic) ? defaultAvatar : comment.User.ProfilePic),
                                                  CommentedDate = comment.Date,
                                                  CommentId = comment.Id,
                                                  Message = comment.CommentBody,
                                                  Postid = comment.PostId

                                              },
                               postLikes = post.Like
                           }).AsEnumerable();

            return newpost.ToList();
            //return post;
        }

        

        // PUT: api/Posts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
           // post.Date = DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
