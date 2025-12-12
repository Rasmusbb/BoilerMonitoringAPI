using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Interface;
using BoilerMonitoringAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace BoilerMonitoringAPI.Services
{
    public class BoilerService : IBoiler
    {

        private readonly BoilerMonitoringAPIContext _context;


        public BoilerService(BoilerMonitoringAPIContext context)
        {
            _context = context;

        }
        string DBNullText = "Database context is not available.";
        public async Task<BoilerDTOID> CreateAsync(BoilerDTO BoilerDTO)
        {
            if (_context.Boilers == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            try
            {
                Boiler boiler = BoilerDTO.Adapt<Boiler>();
                Home home = await _context.Homes.FindAsync(BoilerDTO.HomeID);
                if (home == null)
                {
                    throw new KeyNotFoundException("Home not found");
                }
                _context.Boilers.Add(boiler);
                if (home.Boilers == null)
                {
                    home.Boilers = new List<Boiler>();
                }
                home.Boilers.Add(boiler);
                await _context.SaveChangesAsync();
                BoilerDTOID boilerID = BoilerDTO.Adapt<BoilerDTOID>();
                return boilerID;
            }
            catch
            {
                throw new Exception("Error while creating boiler.");
            }
        }

        public async Task<IEnumerable<BoilerDTOID>> GetAllAsync()
        {

            if (_context.Boilers == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            List<Boiler> Users = await _context.Boilers.ToListAsync();
            return Users.Adapt<IEnumerable<BoilerDTOID>>();

        }

        public async Task<bool> UpdateAsync(Guid BoilerID, BoilerDTO Boiler)
        {
            if (_context.Boilers == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<BoilerDTOID> GetByIdAsync(Guid BoilerID)
        {
            if (_context.Boilers == null)
            {
                throw new InvalidOperationException(DBNullText);
            }

            Boiler boiler = await _context.Boilers.FindAsync(BoilerID);
            BoilerDTOID BoilerDataDTO = boiler.Adapt<BoilerDTOID>();
            return BoilerDataDTO;
        }

        public async Task<bool> DeleteAsync(Guid BoilerID)
        {

            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Boiler Boiler = await _context.Boilers.FindAsync(BoilerID);
            _context.Boilers.Remove(Boiler);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
