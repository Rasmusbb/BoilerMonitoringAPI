using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Models;
using BoilerMonitoringAPI.Services;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BoilerMonitoringAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DeviceController : Controller
    {
        private readonly DeviceService _context;
        public DeviceController(DeviceService context)
        {
            _context = context;
        }



        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create(DeviceDTO device)
        {
            return await _context.CreateAsync(device);
        }



        [HttpPut("FuelData")]
        public async Task<ActionResult<string>> DeviceUpdate(DeviceDTOIDUpdate device)
        {
            if (_context.Devices == null)
            {
                return Problem(isnull);
            }
            Device devices = await _context.Devices.FindAsync(device.DeviceID);
            devices.LastNoice = DateTime.Now;
            await _context.SaveChangesAsync();
            Boiler boilers = _context.Boilers.FirstOrDefault(d => d.DeviceID == device.DeviceID);
            if (boilers == null)
            {
                return Problem("Boiler not found");
            }
            boilers.currentFuelLevel = device.CurrentValue;
            boilers.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("GetBoiler")]
        public async Task<ActionResult<BoilerDTOID>> GetBoiler(Guid BoilerID)
        {
            if (_context.Homes == null)
            {
                return Problem(isnull);
            }
            Boiler boiler = await _context.Boilers.FindAsync(BoilerID);
            BoilerDTOID boilerDTOID = boiler.Adapt<BoilerDTOID>();
            return boilerDTOID;
        }

        private bool HomesExists(Guid id)
        {
            return _context.Homes.Any(e => e.HomeID == id);
        }
    }
}
