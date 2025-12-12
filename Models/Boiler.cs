using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoilerMonitoringAPI.Models
{
    public enum BoilerStatus
    {
        Off,
        On
    }
    public enum BoilerType
    {
        Electric,
        Gas,
        Oil,
        pellet,
        wood
    }
    public class Boiler
    {
        [Key]
        public Guid BoilerID { get; set; }
        public string BoilerName { get; set; }
        public BoilerType BoilerType { get; set; }
        public BoilerStatus BoilerStatus { get; set; }
        public bool IsOpen { get; set; }    
        public double minFuelLevel { get; set; }
        public double maxFuelLevel { get; set; }
        public double currentFuelLevel { get; set; }   
        public DateTime LastUpdated { get; set; }
        [ForeignKey("HomeID")]
        public Guid HomeID { get; set; }
        public Home Home { get; set; }
        

        public Guid DeviceID { get; set; }

        [ForeignKey("DeviceID")]
        public Device Devices { get; set; }
    }
}
