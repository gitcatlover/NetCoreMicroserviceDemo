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
       //���Ocelot����
       .AddOcelot()
       //���consule֧��
       .AddConsul()
       //��ӻ���
       .AddCacheManager(x =>
       {
           x.WithDictionaryHandle();
       })
       .AddPolly();

var app = builder.Build();

//����Ocelot�м��
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
