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
            Client.Authenticator = new JwtAuthenticator(Token);
        }

        protected RestClient Client { get; }

        public string Token
        {
            get
            {
                var request = new RestRequest("auth/authenticate", Method.POST);
                request.AddJsonBody(new
                {
                    email = Configuration["Directus:Email"],
                    password = Configuration["Directus:Password"]
                });
                var response = Client.Execute<TokenResponse>(request);
                return response.Data.Data?.Token ?? throw new ArgumentException();
            }
        }
    }
}
