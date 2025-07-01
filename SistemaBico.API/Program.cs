using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Sistema.Bico.API;

static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logApi.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

        try
        {
            var hostbuilder = CreateHostBuilder(args);
            hostbuilder.Build().Run();
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureKestrel(options =>
                {
                    options.ConfigureEndpointDefaults(endpointOptions =>
                    {
                        endpointOptions.Protocols = HttpProtocols.Http1;
                    });
                });
            })
            .ConfigureServices((context, services) =>
            {
                // Configurações do Host
                services.Configure<HostOptions>(o =>
                {

                });
            });

    public static void ConfigureWebHost(this IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.UseStartup<Startup>();
        webHostBuilder.ConfigureKestrel(c =>
        {
            c.ConfigureEndpointDefaults(x =>
            {
                x.Protocols = HttpProtocols.Http1;
            });
        });
    }
}