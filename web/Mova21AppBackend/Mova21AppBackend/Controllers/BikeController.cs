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
        private readonly IBikeRepository _bikeRepository;

        public BikeController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        [HttpGet]
        public BikeAvailabilities Get()
        {
            return _bikeRepository.GetBikeAvailabilities();
        }

        [HttpPut("")]
        public BikeAvailabilities AdjustCount(ChangeBikeAvailabilityCountModel changeBikeAvailabilityCountModel)
        {
            _bikeRepository.ChangeBikeAvailability(changeBikeAvailabilityCountModel);
            return _bikeRepository.GetBikeAvailabilities();
        }
    }
}
