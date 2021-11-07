using Microsoft.EntityFrameworkCore;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data
{
    public class Mova21AppContext : DbContext
    {
        public Mova21AppContext(DbContextOptions<Mova21AppContext> options)
               : base(options)
        {
        }

        public DbSet<BikeAvailability> BikeAvailabilities { get; set; }
        public DbSet<WeatherEntry> WeatherEntries { get; set; }
    }
}
