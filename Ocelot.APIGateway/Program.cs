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

//���Ocelot����
builder.Services.AddOcelot();

var app = builder.Build();

//����Ocelot�м��
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
