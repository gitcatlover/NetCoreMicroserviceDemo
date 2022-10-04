using Consul;
using RestSharp;

namespace Web.MVC.Helper
{
    public class ServiceHelper : IServiceHelper
    {
        private readonly IConfiguration _configuration;
        public ServiceHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetOrder()
        {
            string[] serviceUrls = { "http://localhost:9060", "http://localhost:9061", "http://localhost:9062" };

            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(_configuration["ConsulSetting:ConsulAddress"]);
            });
            var services = consulClient.Health.Service("OrderService", null, true, null).Result.Response;
            if (!services.Any())
            {
                return await Task.FromResult("【订单服务】服务列表为空");
            }

            //每次随机访问一个服务实例
            var client = new RestClient(serviceUrls[new Random().Next(0, services.Length)]);
            var request = new RestRequest("Api/Order/Get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> GetProduct()
        {
            //string[] serviceUrls = { "http://localhost:9050", "http://localhost:9051", "http://localhost:9052" };
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(_configuration["ConsulSetting:ConsulAddress"]);
            });
            var services = consulClient.Health.Service("ProductService", null, true, null).Result.Response;//健康的服务

            var serviceUrls = services.Select(p => $"http://{p.Service.Address + ":" + p.Service.Port}").ToArray();//订单服务列表
            if (!services.Any())
            {
                return await Task.FromResult("【产品服务】服务列表为空");
            }
            var client = new RestClient(serviceUrls[new Random().Next(0, services.Length)]);
            var request = new RestRequest("Api/Product/Get", Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
