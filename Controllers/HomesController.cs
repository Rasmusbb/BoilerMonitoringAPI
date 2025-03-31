using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.Models;
using BoilerMonitoringAPI.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HomesController : Controller
    {
        private readonly BoilerMonitoringAPIContext _context;

        public HomesController(BoilerMonitoringAPIContext context)
        {
            _context = context;
        }
        string isnull = "Entity set 'DatabaseContext.Homes'  is null.";

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homes.ToListAsync());
        }

        [HttpPost("AddHome")]
        public async Task<ActionResult<HomesDTOID>> AddBoiler(HomesDTO HomeDTO)
        {
            if (_context.Boilers == null)
            {
                return Problem(isnull);
            }
            Homes homes = HomeDTO.Adapt<Homes>();
            _context.Homes.Add(homes);
            await _context.SaveChangesAsync();
            HomesDTOID homeID = HomeDTO.Adapt<HomesDTOID>();
            return CreatedAtAction("GetHome", new { id = homes.HomeID }, homeID);
        }

        [HttpPost("GetHome")]
        public async Task<ActionResult<HomesDTOID>> AddGet(Guid HomeID)
        {
            if (_context.Homes == null)
            {
                return Problem(isnull);
            }
            Homes homes = await _context.Homes.FindAsync(HomeID);
            HomesDTOID HomeDTO = homes.Adapt<HomesDTOID>();
            return HomeDTO;
        }

        private bool HomesExists(Guid id)
        {
            return _context.Homes.Any(e => e.HomeID == id);
        }
    }
}
