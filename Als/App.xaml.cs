using Als.Services;
using Als.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Als
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //Working Host
        private static IHost _Host;
        public static IHost Host => _Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();


        //Posibility of Access to Services
        public static IServiceProvider Services => Host.Services;


        //Start the Host
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var host = Host;
            await host.StartAsync().ConfigureAwait(false); //ConfigureAwait(false) is neccessary
        }


        //Close the Host
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _Host = null;
        }


        //Method for registration of the Services
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            //.AddDataBase(host.Configuration.GetSection("DataBase"))
            .AddServices()
            .AddViewModels()
            ;
    }
}
