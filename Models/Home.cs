using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerMonitoringAPI.Models
{
    public class Home
    {
        [Key]
        public Guid HomeID { get; set; }
        public string HomeName { get; set; }
        public string Address { get; set; }

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        public ICollection<Boiler> Boilers { get; set; }
        public ICollection<User> Members { get; set; }
    }
}
