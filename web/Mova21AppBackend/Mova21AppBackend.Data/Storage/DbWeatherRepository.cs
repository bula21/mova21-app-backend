using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Storage
{
    public class DbWeatherRepository : BaseDbRepository, IWeatherRepository
    {
        public DbWeatherRepository(Mova21AppContext context)
            : base(context)
        {
        }

        public IEnumerable<WeatherEntry> GetWeatherEntriesByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateWeatherEntry(WeatherEntry weatherEntry)
        {
            throw new NotImplementedException();
        }
    }
}
