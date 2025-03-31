using System.ComponentModel.DataAnnotations;

namespace BoilerMonitoringAPI.Models
{
    public class Homes
    {
        [Key]
        public Guid HomeID { get; set; }
        public string HomeName { get; set; }
        public string Address { get; set; }
        public ICollection<Boilers> Boilers { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
