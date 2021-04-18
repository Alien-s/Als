using Microsoft.Extensions.Hosting;
using System;

namespace Als
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)  //Make a Host via Class Host
            //.ConfigureAppConfiguration((host, cfg) => cfg  //Make a configuration of the Application via Lambda
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //Type of the configuration file
            //)
            .ConfigureServices(App.ConfigureServices); //Method for Services Confoguration. But we make his at Class App          
    }
}
