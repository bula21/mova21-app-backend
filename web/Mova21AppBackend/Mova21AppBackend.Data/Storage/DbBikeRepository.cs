using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Data.Storage
{
    public class DbBikeRepository : BaseDbRepository, IBikeRepository
    {
        public DbBikeRepository(Mova21AppContext context) 
            : base(context)
        {
        }

        public BikeAvailabilities GetBikeAvailabilities()
        {
            throw new NotImplementedException();
        }

        public void ChangeBikeAvailability(ChangeBikeAvailabilityCountModel model)
        {
            throw new NotImplementedException();
        }
    }
}
