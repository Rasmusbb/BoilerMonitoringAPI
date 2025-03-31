﻿using System.ComponentModel.DataAnnotations;
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
    public class Boilers
    {
        [Key]
        public Guid BoilerID { get; set; }
        public string BoilerName { get; set; }
        public BoilerType BoilerType { get; set; }
        public BoilerStatus BoilerStatus { get; set; }
        double TargetTemperature { get; set; }

        [ForeignKey("HomeID")]
        public Guid HomeID { get; set; }
        public Homes Home { get; set; }

    }
}
