using BoilerMonitoringAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoilerMonitoringAPI.DTOs
{
    public class BoilerDTO
    {
        public string BoilerName { get; set; }
        public string BoilerType { get; set; }
        public BoilerStatus BoilerStatus { get; set; }
        public Guid HomeID { get; set; }

        public Guid DeviceID { get; set; }
    }


    public class BoilerDTOID : BoilerDTO
    {
        public Guid BoilerID { get; set; }
    }


    public class BoilerDataDTO
    {
        public Guid BoilerID { get; set; }
        public double minFuelLevel { get; set; }
        public double maxFuelLevel { get; set; }
        public double currentFuelLevel { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsOpen { get; set; }
    }

}
