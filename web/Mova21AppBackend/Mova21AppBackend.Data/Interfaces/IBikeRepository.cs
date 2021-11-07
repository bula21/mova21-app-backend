using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Storage
{
    public interface IBikeRepository
    {
        BikeAvailabilities GetBikeAvailabilities();
        void ChangeBikeAvailability(ChangeBikeAvailabilityCountModel model);
    }
}
