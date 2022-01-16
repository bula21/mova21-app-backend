using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;
using RestSharp;

namespace Mova21AppBackend.Data.Storage
{
    public class DirectusWeatherRepository : BaseDirectusRepository, IWeatherRepository
    {
        const string WeatherUrl = "items/weather";

        public DirectusWeatherRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<WeatherEntries> GetWeatherEntriesByDateRange(DateTime startDate, DateTime endDate)
        {
            var request = new RestRequest(WeatherUrl);
            var response = await Client.ExecuteGetAsync<WeatherEntriesResponse>(request);
            return new WeatherEntries
            {
                Entries = response.Data.Data?.Select(x => x.ToWeatherEntry()) ?? Enumerable.Empty<WeatherEntry>()
            };
        }

        public async Task UpdateWeatherEntry(WeatherEntry model)
        {
            var patchRequest = new RestRequest($"{WeatherUrl}/{model.Id}", Method.PATCH)
                .AddJsonBody(model);
            await Client.ExecuteAsync<WeatherEntryResponse>(patchRequest);
        }

        public async Task DeleteWeatherEntry(int id)
        {
            var deleteRequest = new RestRequest($"{WeatherUrl}/{id}", Method.DELETE);
            await Client.ExecuteAsync(deleteRequest);
        }

        public async Task<WeatherEntry> CreateWeatherEntry(WeatherEntry model)
        {
            var createRequest = new RestRequest($"{WeatherUrl}/", Method.POST)
                .AddJsonBody(new WeatherEntryResponseData
                {
                    Date = model.Date,
                    DayTime = model.DayTime,
                    Temperature = model.Temperature,
                    Weather = model.Weather,
                });
            var createResponse = await Client.ExecuteAsync<WeatherEntryResponse>(createRequest);
            return createResponse.Data.Data?.ToWeatherEntry() ?? throw new ArgumentNullException(nameof(createResponse.Data.Data));
        }
    }
}
