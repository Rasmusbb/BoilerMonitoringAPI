using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Models;
using BoilerMonitoringAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BoilerController : Controller
    {
        private readonly BoilerService _context;
        public BoilerController(BoilerService context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BoilerDTOID>> Create(BoilerDTO BoilerDTO)
        {
           return await _context.CreateAsync(BoilerDTO);
        }

        [Authorize]
        [HttpGet("GetBoiler")]
        public async Task<ActionResult<BoilerDataDTO>> GetBoiler(Guid BoilerID)
        {
            return await _context.GetBoiler(BoilerID);
        }
    }
}
