using BackEndCharityProject.Models.Posts.PostsHelpCRUD;
using BackEndCharityProject.Models.Posts;
using BackEndCharityProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEndCharityProject.Models;

namespace BackEndCharityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostHelpController : ControllerBase
    {
        private readonly IPostHelpService _postHelpService;
        public PostHelpController(IPostHelpService _postHelpService)
        {
            this._postHelpService = _postHelpService;
        }

        [HttpPost("postadd")]
        public async Task<IActionResult> PostAdd(PostHelpAdd post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool res = await _postHelpService.PostCreate(post);
            if (res)
            {
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("getallposts")]
        public IActionResult GetAllPosts()
        {
            IEnumerable<PostHelp> posts = _postHelpService.GetAllPosts();
            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("postdelete/{id:int}")]
        public async Task<IActionResult> PostDelete(int id)
        {
            if (await _postHelpService.PostDelete(id) == true)
            {
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("postget/{id:int}")]
        public async Task<IActionResult> PostGet(int id)
        {
            PostHelpRead pg = await _postHelpService.PostRead(id);
            if (pg != null)
            {
                return Ok(pg);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("postupdate/{id:int}")]
        public async Task<IActionResult> PostUpdate(int id, [FromBody] PostHelpUpdate post)
        {
            if (await _postHelpService.PostUpdate(id, post) == true)
            {
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("getuserposts/{id:int}")]
        public async Task<IActionResult> UserPostsGet(int id)
        {
            IEnumerable<PostHelpRead> posts = await _postHelpService.UserPostsGet(id);
            if (posts != null)
            {
                return Ok(posts);
            }
            return BadRequest(ModelState);
        }
    }
}
