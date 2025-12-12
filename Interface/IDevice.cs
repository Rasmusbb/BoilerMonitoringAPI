using BoilerMonitoringAPI.DTOs;

namespace BoilerMonitoringAPI.Interface
{
    public interface IDevice : ICRUD<DeviceDTO, DeviceDTOID>
    {
        Task<bool> FuelUpdate(DeviceDTOIDUpdate device);
    }
}
