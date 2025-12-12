using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Interface;
using BoilerMonitoringAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BoilerMonitoringAPI.Services
{
    public class HomeService : IHome
    {
        private readonly BoilerMonitoringAPIContext _context;


        public HomeService(BoilerMonitoringAPIContext context)
        {
            _context = context;

        }

        string DBNullText = "Database context is not available.";
        public async Task<HomesDTOID> CreateAsync(HomesDTO homeDTO)
        {
            if (_context.Homes == null || _context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Home home = homeDTO.Adapt<Home>();
            _context.Homes.Add(home);
            await _context.SaveChangesAsync();
            return home.Adapt<HomesDTOID>();
        }

        public async Task<HomesDTOID> GetByIdAsync(Guid HomeID)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Home homes = await _context.Homes.FindAsync(HomeID);
            HomesDTOID HomeDTO = homes.Adapt<HomesDTOID>();
            return HomeDTO;
        }

        public async Task<IEnumerable<HomesDTOID>> GetAllAsync()
        {

            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            List<Boiler> Users = await _context.Boilers.ToListAsync();
            return Users.Adapt<IEnumerable<HomesDTOID>>();

        }
        public async Task<UserDTOID> AddUserByEmail(HomeUser HomeUser)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            try
            {
                Home home = await _context.Homes.FindAsync(HomeUser.HomeID);
                User user = _context.Users.FirstOrDefault(U => U.Email == HomeUser.Email.ToLower());
                if (home == null || user == null)
                {
                    throw new KeyNotFoundException("Home or User not found");
                }
                home.Members.Add(user);
                await _context.SaveChangesAsync();
                HomesDTOID homeID = home.Adapt<HomesDTOID>();
                return user.Adapt<UserDTOID>();
            }
            catch
            {
                throw new Exception("Error while adding user to home.");
            }
        }


        public async Task<ActionResult<bool>> RemoveUser(HomeUser HomeUser)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            try
            {
                Home home = await _context.Homes.FindAsync(HomeUser.HomeID);
                User user = _context.Users.FirstOrDefault(U => U.Email == HomeUser.Email.ToLower());
                home.Members.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error while removeing User.");
            }
        }

        public async Task<ActionResult<string>> GenerateInviteCode(Guid HomeID)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Home home = await _context.Homes.FindAsync(HomeID);
            if (home == null)
            {
                throw new KeyNotFoundException("Home not found");
            }
            await _context.SaveChangesAsync();
            throw new NotImplementedException("Invite code generation is not implemented yet. So get to work BB");
        }

        public async Task<bool> UpdateAsync(Guid HomeID, HomesDTO HomeDTO)
        {
            throw new NotImplementedException("Update home is not implemented yet. So get to work BB");
        }

        public async Task<ActionResult<bool>> DeleteAsync(Guid HomeID)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            _context.Homes.Remove(await _context.Homes.FindAsync(HomeID));
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
