using BoilerMonitoringAPI.Models;

namespace BoilerMonitoringAPI.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UserDTOID : UserDTO
    {
        public Guid UserID { get; set; }
    }

    public class UserDTOIDTokens : UserDTOID
    {
        public string RefreshToken { get; set; }
        public DateTime RefeshTokenExpiryTime { get; set; }

        string? accessToken { get; set; }
    }  

    public class RefreshTokenUser
    {
        public Guid UserID { get; set; }
        public string RefreshToken { get; set; }
    }

    public class UserPassword : UserDTO
    {
        public string Password { get; set; }
    }

    public class ÜserTokens
    {
        public string accessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
