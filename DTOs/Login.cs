namespace BoilerMonitoringAPI.DTOs
{
    public class UserLoingObject()
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserTokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserTokens(string AccessToken, string RefreshToken)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken; 
        }
    }
}
