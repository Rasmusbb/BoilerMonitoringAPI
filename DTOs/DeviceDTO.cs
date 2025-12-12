namespace BoilerMonitoringAPI.DTOs
{
    public class DeviceDTO
    {
        public string DeviceName { get; set; }

    }

    public class DeviceDTOIDUpdate
    {
        public Guid DeviceID { get; set; }
        public double CurrentValue { get; set; }
    }

    public class DeviceDTOID : DeviceDTO
    {
        public Guid DeviceID { get; set; }
        public bool IsActive { get; set; }
        public DateTime firstNoice { get; set; }
        public DateTime LastNoice { get; set; }
    }
}
