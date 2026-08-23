using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuInTech_TEST.Infrastructure;
using RuInTech_TEST.UI;
using System;
using System.Windows.Forms;

namespace RuInTech_TEST
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();

            var mainForm = host.Services.GetRequiredService<AssetsForm>();
            Application.Run(mainForm);
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddAssetsInfrastructure();
                    services.AddUIForms();
                });
        }
    }
}
