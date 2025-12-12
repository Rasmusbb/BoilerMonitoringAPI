
using BoilerMonitoringAPI.Data;
using BoilerMonitoringAPI.DTOs;
using BoilerMonitoringAPI.Interface;
using BoilerMonitoringAPI.Models;
using Mapster;


namespace BoilerMonitoringAPI.Services
{
    public class DeviceService : IDevice
    {
        private readonly BoilerMonitoringAPIContext _context;


        public DeviceService(BoilerMonitoringAPIContext context)
        {
            _context = context;

        }
        string DBNullText = "Database context is not available.";



        public async Task<DeviceDTOID> CreateAsync(DeviceDTO Device)
        {
            if (_context.Devices == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Device devices = new Device();
            devices.DeviceName = Device.DeviceName;
            devices.IsActive = true;
            devices.firstNoice = DateTime.Now;
            devices.LastNoice = DateTime.Now;
            _context.Devices.Add(devices);
            await _context.SaveChangesAsync();
            return Device.Adapt<DeviceDTOID>();
        }

        public async Task<DeviceDTOID> GetByIdAsync(Guid DeviceID)
        {
            if (_context.Devices == null)
            {
                throw new InvalidOperationException(DBNullText);
            }

            Device Device = await _context.Devices.FindAsync(DeviceID);
            DeviceDTOID DeviceDTOID = Device.Adapt<DeviceDTOID>();
            return DeviceDTOID;
        }

        public async Task<bool> FuelUpdate(DeviceDTOIDUpdate device)
        {
            if (_context.Devices == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Device devices = await _context.Devices.FindAsync(device.DeviceID);
            devices.LastNoice = DateTime.Now;
            await _context.SaveChangesAsync();
            Boiler boilers = _context.Boilers.FirstOrDefault(d => d.DeviceID == device.DeviceID);
            if (boilers == null)
            {
                throw new KeyNotFoundException($"Device not found.");
            }
            boilers.currentFuelLevel = device.CurrentValue;
            boilers.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Guid DeviceID, DeviceDTO deviceDTO)
        {
            if (_context.Devices == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid DeviceID)
        {

            if (_context.Devices == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Device Device = await _context.Devices.FindAsync(DeviceID);
            _context.Devices.Remove(Device);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
