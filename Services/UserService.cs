using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Interface;
using BoilerMonitoringAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Text;
using IUser = BoilerMonitoringAPI.Interface.IUser;


namespace BoilerMonitoringAPI.Services
{
    public class UserService : IUser
    {
        private readonly BoilerMonitoringAPIContext _context;
        private readonly Auth _auth;
      
        public UserService(BoilerMonitoringAPIContext context, Auth auth)
        {
            _context = context;
            _auth = auth;
        }

        string DBNullText = "Database context is not available.";
        public async Task<UserDTOID> CreateAsync(UserDTO UserDTO)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new InvalidOperationException(DBNullText);
                }
                UserDTO.Email = UserDTO.Email.ToLower();
                User User = UserDTO.Adapt<User>();
                _context.Users.Add(User);
                User.Password = Hash(User.Password, User.UserID.ToString());
                await _context.SaveChangesAsync();
                return User.Adapt<UserDTOID>();
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException("Conflict while saving user. Possibly duplicate data.", dbEx);
            }
        }

        public async Task<string> ChangePassword(Guid UserID, string NewPassword)
        {
            User User = _context.Users.Find(UserID);
            if (User == null)
            {
                throw new KeyNotFoundException($"User with ID '{UserID}' was not found.");
            }
            User.Password = Hash(User.Password, User.UserID.ToString());
            await _context.SaveChangesAsync();
            return "Password changed";
        }


        public async Task<ActionResult<List<UserDTOID>>> GetHomeMembers(Guid HomeID)
        {
            if (_context.Homes == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Home home = await _context.Homes.Include(h => h.Members).FirstOrDefaultAsync(h => h.HomeID == HomeID);
            return home.Members.Adapt<List<UserDTOID>>();
        }

        public async Task<UserTokens> Login(UserLoingObject UserLogin)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            UserLogin.Email = UserLogin.Email.ToLower();
            var user = _context.Users.FirstOrDefault(u => u.Email == UserLogin.Email);
            UserDTOID UserDTO = user.Adapt<UserDTOID>();
            
            if (user != null)
            {
                string hash = Hash(UserLogin.Password, user.UserID.ToString());
                if (hash == user.Password)
                {
                    RefreshToken RefreshToken = await CreateRefreshToken(user);
                    return new UserTokens(_auth.GenerateJwtToken(UserDTO), RefreshToken.Token);
                }
            }
            return null;
        }
        public async Task<UserTokens> RefreshToken(RefreshTokenUser Token)
        {
            User user = _context.Users.Find(Token.UserID);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {Token.UserID} was not found.");

            }
            if (Hash(Token.RefreshToken, user.UserID.ToString()) == user.RefreshToken)
            {
                if (user.RefeshTokenExpiryTime < DateTime.UtcNow)
                {
                    user.RefreshToken = null;
                    user.RefeshTokenExpiryTime = null;
                    await _context.SaveChangesAsync();
                    return null;
                }
                else
                {
                    RefreshToken RefreshToken = await CreateRefreshToken(user);
                    return new UserTokens(_auth.GenerateJwtToken(user.Adapt<UserDTOID>()), RefreshToken.Token);
                }
            }
            return null;
        }

        public async Task<IEnumerable<UserDTOID>> GetAllAsync()
        {

            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            List<User> Users = await _context.Users.Where(u => u.UserID != Guid.Parse("2c08577b-c673-416e-031b-08ddfcc99d40")).ToListAsync();
            return Users.Adapt<IEnumerable<UserDTOID>>();

        }

        public async Task<UserDTOID> GetByIdAsync(Guid UserID)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User user = await _context.Users.FindAsync(UserID);
            UserDTOID userDTO = user.Adapt<UserDTOID>();
            return userDTO;
        }

        public async Task<bool> UpdateAsync(Guid UserID, UserDTO User)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid UserID)
        {

            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User User = await _context.Users.FindAsync(UserID);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Logout(Guid UserID)
        {
            User user = await _context.Users.FindAsync(UserID);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID '{UserID}' was not found.");
            }
            user.RefeshTokenExpiryTime = DateTime.UtcNow;
            return true;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            RefreshToken RefeshToken = new RefreshToken();
            user.RefeshTokenExpiryTime = RefeshToken.ExpiryTime;
            user.RefreshToken = Hash(RefeshToken.Token, user.UserID.ToString());
            await _context.SaveChangesAsync();
            return RefeshToken;
        }

        public string Hash(string password, string salt)
        {

            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
            }
            return builder.ToString();
        }

       

    }

}

