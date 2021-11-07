using System.ComponentModel.DataAnnotations;

namespace Mova21AppBackend.Data.Models
{
    public class BikeAvailability
    {
        [Required]
        public int Id { get; set; }
        public int AvailableCount { get; set; }
        public string Type { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
