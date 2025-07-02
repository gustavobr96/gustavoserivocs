using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace Sistema.Bico.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.Console()
                .WriteTo.File("logApi.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Iniciando aplicação...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Aplicação falhou ao iniciar.");
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
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureEndpointDefaults(endpointOptions =>
                        {
                            endpointOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });

                        //  Definindo limites para evitar erro de socket
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                        serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(120);
                        serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
