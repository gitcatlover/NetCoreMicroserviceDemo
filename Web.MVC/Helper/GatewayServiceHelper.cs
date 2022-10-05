using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Web.MVC.Helper
{
    public class GatewayServiceHelper : IServiceHelper
    {
        private readonly IConfiguration _configuration;

        public GatewayServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetOrder()
        {
            var client = new RestClient("http://localhost:9070");
            var request = new RestRequest("api/order/get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            var client = new RestClient("http://localhost:9070");
            var request = new RestRequest("api/product/get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public void GetServices()
        {
            throw new NotImplementedException();
        }
    }
}
