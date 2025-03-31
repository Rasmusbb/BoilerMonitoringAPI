namespace BoilerMonitoringAPI.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        ICollection<Homes> Homes { get; set; }  
    }
}
