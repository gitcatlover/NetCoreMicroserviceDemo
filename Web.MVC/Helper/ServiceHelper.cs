using RestSharp;

namespace Web.MVC.Helper
{
    public class ServiceHelper : IServiceHelper
    {
        public async Task<string> GetOrder()
        {
            string[] serviceUrls = { "http://localhost:9060", "http://localhost:9061", "http://localhost:9062" };
            //每次随机访问一个服务实例
            var client = new RestClient(serviceUrls[new Random().Next(0, 3)]);
            var request = new RestRequest("Api/Order/Get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            string[] serviceUrls = { "http://localhost:9050", "http://localhost:9051", "http://localhost:9052" };
            var client = new RestClient(serviceUrls[new Random().Next(0, 3)]);
            var request = new RestRequest("Api/Product/Get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
