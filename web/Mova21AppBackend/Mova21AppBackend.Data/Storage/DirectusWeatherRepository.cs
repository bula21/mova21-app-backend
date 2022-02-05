﻿using Microsoft.Extensions.Configuration;
using MoreLinq;
using MoreLinq.Experimental;
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

            var existingEntries = (response.Data?.Data?.Select(x => x.ToWeatherEntry()) ?? Enumerable.Empty<WeatherEntry>())
                .ToList();

            var newEntries = GetDateDayTimeCombinationsInRange(startDate, endDate)
                .Where(dateDayTimeCombination =>
                    !existingEntries.Any(existingEntry => existingEntry.Date.Date == dateDayTimeCombination.Date 
                                                          && existingEntry.DayTime == dateDayTimeCombination.DayTime))
                .Select(async dateDayTimeCombination => await CreateWeatherEntry(new WeatherEntry
                {
                    Date = dateDayTimeCombination.Date,
                    DayTime = dateDayTimeCombination.DayTime,
                    Temperature = 20,
                    Weather = WeatherType.Sun
                }))
                .Await();

            return new WeatherEntries
            {
                Entries = existingEntries.Concat(newEntries).OrderBy(x => x.Date).ThenBy(x => x.DayTime)
            };
        }

        public IEnumerable<(DateTime Date, DayTime DayTime)> GetDateDayTimeCombinationsInRange(DateTime startDate, DateTime endDate)
        {
            for (int i = 0; i < (endDate - startDate).TotalDays; i++)
            {
                yield return (startDate.AddDays(i).Date, DayTime.Morning);
                yield return (startDate.AddDays(i).Date, DayTime.Midday);
                yield return (startDate.AddDays(i).Date, DayTime.Evening);
            }
        }

        public async Task UpdateWeatherEntry(WeatherEntry model)
        {
            var patchRequest = new RestRequest($"{WeatherUrl}/{model.Id}", Method.Patch)
                .AddJsonBody(model);
            await Client.ExecuteAsync<WeatherEntryResponse>(patchRequest);
        }

        public async Task DeleteWeatherEntry(int id)
        {
            var deleteRequest = new RestRequest($"{WeatherUrl}/{id}", Method.Delete);
            await Client.ExecuteAsync(deleteRequest);
        }

        public async Task<WeatherEntry> CreateWeatherEntry(WeatherEntry model)
        {
            var createRequest = new RestRequest($"{WeatherUrl}/", Method.Post)
                .AddJsonBody(new WeatherEntryResponseData
                {
                    Date = model.Date,
                    DayTime = model.DayTime,
                    Temperature = model.Temperature,
                    Weather = model.Weather,
                });
            var createResponse = await Client.ExecuteAsync<WeatherEntryResponse>(createRequest);
            if (createResponse.IsSuccessful)
            {
                return createResponse.Data.Data?.ToWeatherEntry() ??
                       throw new ArgumentNullException(nameof(createResponse.Data.Data));
            }
            else
            {
                throw new Exception("Failed to create weather entry in Directus: ", createResponse.ErrorException)
            }
        }
    }
}
