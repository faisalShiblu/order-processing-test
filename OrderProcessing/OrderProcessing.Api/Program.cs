using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessing.Api
{
    public class Program
    {
        // public static IConfiguration Configuration { get; private set; }
        public static void Main(string[] args)
        {

            //Configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", false, true)
            //    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
            //        true)
            //    .AddCommandLine(args)
            //    .AddEnvironmentVariables()
            //    .Build();

            //// Configure serilog
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(Configuration)
            //    .Enrich.FromLogContext()
            //    .Enrich.WithExceptionDetails()
            //    .Enrich.WithMachineName()
            //    .CreateLogger();

            //try
            //{
            //    Log.Information("Starting up...");
            //    CreateHostBuilder(args).Build().Run();
            //    Log.Information("Shutting down...");
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Api host terminated unexpectedly");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

            //CreateHostBuilder(args).Build().Run();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             //Host.CreateDefaultBuilder(args)
             //    .ConfigureWebHostDefaults(webBuilder =>
             //    {
             //        webBuilder.UseStartup<Startup>();
             //    });
             Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSerilog(); 
            });
    }
}
