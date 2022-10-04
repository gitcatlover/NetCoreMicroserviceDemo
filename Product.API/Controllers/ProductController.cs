using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Xml.Linq;

namespace Product.API.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = $"【产品服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}--{Request.HttpContext.Connection.LocalIpAddress}:{Request.HttpContext.Connection.LocalPort}";
            return Ok(result);
        }
    }
    //项目根目录启动consul, consul.exe agent -dev
    //1, docker build -t productapi -f ./Product.API/Dockerfile .    生成一个image
    //2, docker run -d -p 9050:80 --name productservice productapi   运行实例
    //3,简单集群服务，使用docker运行多个服务实例：
    //docker run -d -p 9061:80 --name orderservice1 orderapi
    //docker run -d -p 9062:80 --name orderservice2 orderapi
    //docker run -d -p 9051:80 --name productservice1 productapi
    //docker run -d -p 9052:80 --name productservice2 productapi
}
