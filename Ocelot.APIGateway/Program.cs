using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    //config.Sources.Clear();
    config.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
});
//IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json", optional: false, reloadOnChange: true).Build();
//builder.Services.AddSingleton(configuration);

builder.Services
       //添加Ocelot服务
       .AddOcelot()
       //添加consule支持
       .AddConsul()
       //添加缓存
       .AddCacheManager(x =>
       {
           x.WithDictionaryHandle();
       })
       .AddPolly();

var app = builder.Build();

//设置Ocelot中间件
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
