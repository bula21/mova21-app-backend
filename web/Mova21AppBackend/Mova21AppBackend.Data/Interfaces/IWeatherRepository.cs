using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Interfaces
{
    public interface IWeatherRepository
    {
        public IEnumerable<WeatherEntry> GetWeatherEntriesByDateRange(DateTime startDate, DateTime endDate);
        public void UpdateWeatherEntry(WeatherEntry weatherEntry);
    }
}
