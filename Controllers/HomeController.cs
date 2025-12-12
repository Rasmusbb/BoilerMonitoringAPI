

using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Models;
using BoilerMonitoringAPI.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HomeController : Controller
    {
        private readonly HomeService _context;
        public HomeController(HomeService context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("CreateAsync")]
        public async Task<ActionResult<HomesDTOID>> CreateAsync(HomesDTO homeDTO)
        {
            Guid userID = Guid.Parse(User.FindFirst("userID")?.Value);
            return await _context.CreateAsync(homeDTO);
        }

        [Authorize]
        [HttpGet("GetHome")]
        public async Task<ActionResult<HomesDTOID>> GetHome(Guid HomeID)
        {
           return await _context.GetByIdAsync(HomeID);
        }

        [Authorize]
        [HttpPatch("AddUserByEmail")]
        public async Task<ActionResult<UserDTOID>> AddUserByEmail(HomeUser homeUser)
        {
            return await _context.AddUserByEmail(homeUser);
        }


        [Authorize]
        [HttpPatch("RemoveUser")]
        public async Task<ActionResult<bool>> RemoveUser(HomeUser HomeUser)
        {
            return await _context.RemoveUser(HomeUser);
        }



        [Authorize]
        [HttpPost("GenerateinviteCode")]
        public async Task<ActionResult<string>> GenerateInviteCode(Guid HomeID)
        {
            return await _context.GenerateInviteCode(HomeID);
        }

        [Authorize]
        [HttpDelete("DeleteAsync")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid HomeID)
        {
            return await _context.DeleteAsync(HomeID);
        }
    }
}
