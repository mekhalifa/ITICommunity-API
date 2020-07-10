using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITICommunity.Models;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public LikesController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/Likes/postlike/5
        [HttpGet("postlike/{postid}")]
        public IEnumerable<Like> GetpostLike(int postid)
        {
            var postToFind = _context.Posts.Find(postid);

            if (postToFind == null)
            {
                //return NotFound();
            }
            var postLike = (from like in _context.Likes.ToList()
                            where like.PostId == postid
                            select new Like { Id = like.Id, UserId = like.UserId, PostId = like.PostId }).ToList();
            return postLike;
        }

        // GET: api/Likes/toplikes
    //    [HttpGet("toplikes")]
    //    public  IEnumerable<Like> GetTopLikes()
    //    {
    //        //select PostID,count(PostID) from [dbo].[Like] group by PostID 
    //        //var topLike = _context.getTopPostLikes.FromSqlRaw("getTopPostLikes");

    //        //return topLike;
    //        var playersPerTeam =
    //(from player in _context.Like
    // group player by player.PostId into playerGroup
    // select new
    // {
    //     PostId = playerGroup.Key,
    //     Count = playerGroup.Count(),
    // }).ToList();
    //        return playersPerTeam;
    //    }


        // GET: api/Likes/commentlike/5
        [HttpGet("commentlike{commentid}")]
        public IEnumerable<Like> GetcommentLike(int commentid)
        {
            var comm =  _context.Comments.Find(commentid);

            if (comm == null)
            {
               // return NotFound();
            }
            var commentLike = (from like in _context.Likes.ToList()
                               where like.CommentId == commentid
                               select new Like { Id = like.Id, UserId = like.UserId, CommentId = like.CommentId }).ToList();


            return commentLike;
        }

        // PUT: api/Likes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.Id)
            {
                return BadRequest();
            }

            _context.Entry(like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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

        // POST: api/Likes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Like>> DeleteLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return like;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
