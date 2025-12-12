using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoilerMonitoringAPI.Services;

namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private readonly string DsetNull = "Entity set 'DatabaseContext.User'  is null.";
        private readonly UserService _context;
        public UserController(UserService context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTOIDTokens>> Login(UserLoingObject UserLogin)
        {
            UserTokens Tokens = await _context.Login(UserLogin);
            if (Tokens == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(Tokens);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> LoginByRefreshToken(RefreshTokenUser Token)
        {
            try
            {
                UserTokens Tokens = await _context.RefreshToken(Token);
                if (Tokens == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(Tokens);
                }
            }
            catch (KeyNotFoundException Kex)
            {
                return StatusCode(404, $"UserID didn't match any in the stystem {Kex}");
            }
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> Create(UserPassword UserDTO)
        {
            try
            {
                UserDTOID User = await _context.CreateAsync(UserDTO);
                if (User == null)
                {
                    return BadRequest();
                }
                return CreatedAtAction(nameof(GetById), new { id = User.UserID }, User);
            }
            catch (DbUpdateException dbex)
            {
                return Conflict($"An error occurred while saving the entity changes {dbex}");
            }



        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<UserDTOID>> GetById(Guid UserID)
        {
            UserDTOID User = await _context.GetByIdAsync(UserID);
            if (User == null)
                return NotFound();

            return Ok(User);
        }
    }
}
