using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova21AppBackend.Data.Models
{
    public class WeatherEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DayTime DayTime { get; set; }
        public double Temperature { get; set; }
        public WeatherType Weather { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }

    public enum WeatherType
    {
        Cloud,
        CloudSun,
        CloudSunRain,
        CloudRain,
        Sun,
        Thunderstorm,
        Fog,
        Snow
    }

    public enum DayTime
    {
        Morning,
        Midday,
        Evening,
    }
}
