using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    //config.Sources.Clear();
    config.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
});
//IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json", optional: false, reloadOnChange: true).Build();
//builder.Services.AddSingleton(configuration);

//添加Ocelot服务
builder.Services.AddOcelot();

var app = builder.Build();

//设置Ocelot中间件
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
