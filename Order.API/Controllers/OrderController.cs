using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IConfiguration _configuration;
        public OrderController(ILogger<OrderController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = $"【订单服务】{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}--{Request.HttpContext.Connection.LocalIpAddress}:{_configuration["ConsulSetting:ServicePort"]}";
            return Ok(result);
        }
    }
}
