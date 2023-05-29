using BackEndCharityProject.Models;
using BackEndCharityProject.Models.AuthModels;
using BackEndCharityProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCharityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool res = await _userService.UserRegister(user);
            if (res)
            {
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int? res = _userService.UserLogin(user);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("userget/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            User user = await _userService.GetCertainUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("userallget")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (await _userService.GetAllUsers() is List<User> users)
            {
                return Ok(users);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("userupdate/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _userService.UpdateUser(id, user) == true)
            {
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("setrating/{id_origin:int}/{id_vote:int}/{rating:double}")]
        public async Task<IActionResult> SetRating([FromRoute]int id_origin, [FromRoute]int id_vote, [FromRoute]double rating)
        {
            if (await _userService.SetRating(id_origin, id_vote, rating) == true)
            {
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
