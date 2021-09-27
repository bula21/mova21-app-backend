using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.Storage;

namespace Mova21AppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController
    {
        private readonly IStorage _storage;

        public BikeController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public BikeAvailabilities Get()
        {
            return _storage.GetBikeAvailabilities();
        }

        [HttpPut("")]
        public BikeAvailabilities AdjustCount(ChangeBikeAvailabilityCountModel changeBikeAvailabilityCountModel)
        {
            _storage.ChangeBikeAvailability(changeBikeAvailabilityCountModel);
            return _storage.GetBikeAvailabilities();
        }
    }
}
