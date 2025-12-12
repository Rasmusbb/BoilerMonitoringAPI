using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerMonitoringAPI.Models
{
    public class Device
    {
        [Key]
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; }
        public Boiler Boiler { get; set; }

        public bool IsActive { get; set; }  
        public DateTime firstNoice { get; set; }
        public DateTime LastNoice { get; set; }
    }
}
