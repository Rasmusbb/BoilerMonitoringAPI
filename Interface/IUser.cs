using BoilerMonitoringAPI.DTOs;

namespace BoilerMonitoringAPI.Interface
{
    public interface IUser : ICRUD<UserDTO,UserDTOID>
    {
        Task<UserTokens> Login(UserLoingObject UserLogin);
        Task<bool> Logout(Guid UserID);
        Task<UserTokens> RefreshToken(RefreshTokenUser Token);
        Task<string> ChangePassword(Guid UserID, string NewPassword);
    }
}
