using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.Models;
using BoilerMonitoringAPI.DTOs;

namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BoilersController : Controller
    {
        private readonly BoilerMonitoringAPIContext _context;

        public BoilersController(BoilerMonitoringAPIContext context)
        {
            _context = context;
        }

        string isnull = "Entity set 'DatabaseContext.Boilers'  is null.";

        [HttpPost("AddBoiler")]
        public async Task<ActionResult<BoilerDTOID>> AddBoiler(BoilerDTO BoilerDTO)
        {
            if (_context.Boilers == null)
            {
                return Problem(isnull);
            }
            Boilers boiler = BoilerDTO.Adapt<Boilers>();
            _context.Boilers.Add(boiler);
            await _context.SaveChangesAsync();
            BoilerDTOID boilerID = BoilerDTO.Adapt<BoilerDTOID>();
            return CreatedAtAction("GetBoiler", new { id = boiler.BoilerID }, boilerID);
        }

        [HttpPost("GetBoiler")]
        public async Task<ActionResult<BoilerDTOID>> GetBoiler(Guid BoilerID)
        {
            if (_context.Homes == null)
            {
                return Problem(isnull);
            }
            Boilers boiler = await _context.Boilers.FindAsync(BoilerID);
            BoilerDTOID boilerDTOID = boiler.Adapt<BoilerDTOID>();
            return boilerDTOID;
        }

        private bool BoilersExists(Guid id)
        {
            return _context.Boilers.Any(e => e.BoilerID == id);
        }
    }
}
