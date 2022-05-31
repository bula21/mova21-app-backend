using Microsoft.Extensions.Configuration;
using MoreLinq;
using MoreLinq.Experimental;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;
using RestSharp;

namespace Mova21AppBackend.Data.Storage;

public class DirectusPermissionRepository : BaseDirectusRepository, IPermissionRepository
{
    const string WeatherUrl = "items/Permission";

    public DirectusPermissionRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<WeatherEntries> GetPermission()
    {
        var request = new RestRequest(WeatherUrl);
        var response = await Client.ExecuteGetAsync<WeatherEntriesResponse>(request);

        var permissionEntry = response.Data?.Data?.Single();
    }
}