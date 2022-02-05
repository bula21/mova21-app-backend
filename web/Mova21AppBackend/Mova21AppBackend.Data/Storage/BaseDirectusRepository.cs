using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.RestModels;
using RestSharp;
using RestSharp.Authenticators;

namespace Mova21AppBackend.Data.Storage
{
    public abstract class BaseDirectusRepository
    {
        public IConfiguration Configuration { get; }

        protected BaseDirectusRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            Client = new RestClient(Configuration["Directus:BaseUrl"]);
            Client.Authenticator = new JwtAuthenticator(GetToken().Result);
        }

        protected RestClient Client { get; }

        public async Task<string> GetToken()
        {

            var request = new RestRequest("auth/authenticate", Method.Post);
            request.AddJsonBody(new
            {
                email = Configuration["Directus:Email"],
                password = Configuration["Directus:Password"]
            });
            var response = await Client.ExecuteAsync<TokenResponse>(request);
            return response.Data.Data?.Token ?? throw new ArgumentException();
        }
    }
}
