using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PhotoBackup.Library.SettingsModels;
using PhotoBackup.Library.Interfaces;
using PhotoBackup.Library.Extensions;


namespace PhotoBackup.WinForm
{
    public static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ServiceProvider = CreateHostBuilder().Build().Services;
            Application.Run(ServiceProvider.GetService<Dashboard>()!);
        }

        private static IHostBuilder CreateHostBuilder()
        {
            IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json", false)
                    .Build();

            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton(config);
                    services.AddSingleton<Dashboard>();
                });
        }

        private static Settings BuildSettingsFromConfig(IConfiguration config)
        {
            Settings settings = new()
            {
                DirectoryPaths = config.BuildSettings<DirectoryPaths>(),
            };

            return settings;
        }
    }
}