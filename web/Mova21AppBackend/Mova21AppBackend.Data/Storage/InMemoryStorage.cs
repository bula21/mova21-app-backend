using System.Collections.Generic;
using System.Linq;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Storage
{
    public class InMemoryStorage : IStorage
    {
        private static readonly Dictionary<string, int> Bikes = new()
        {
            ["Cargo"] = 10,
            ["Regular"] = 100
        };

        public BikeAvailabilities GetBikeAvailabilities()
        {
            return new BikeAvailabilities
            {
                Availabilities = Bikes.Select(x => new BikeAvailability { AvailableCount = x.Value, Type = x.Key })
            };
        }

        public void ChangeBikeAvailability(ChangeBikeAvailabilityCountModel model)
        {
            if (Bikes.ContainsKey(model.Type))
            {
                Bikes[model.Type] += model.AmountChange;
            }
        }
    }
}
