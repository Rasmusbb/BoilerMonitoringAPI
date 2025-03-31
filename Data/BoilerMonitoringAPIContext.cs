using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoilerMonitoringAPI.Models;

namespace BoilerMonitoringAPI.Data
{
    public class BoilerMonitoringAPIContext : DbContext
    {
        public BoilerMonitoringAPIContext (DbContextOptions<BoilerMonitoringAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BoilerMonitoringAPI.Models.Boilers> Boilers { get; set; } = default!;
        public DbSet<BoilerMonitoringAPI.Models.Homes> Homes { get; set; } = default!;
    }
}
