using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Infrastructure;
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

            using (var serviceProvider = BuildServiceProvider())
            {
                var mainForm = serviceProvider.GetRequiredService<AssetsForm>();
                Application.Run(mainForm);
            }
        }

        /// <summary>
        /// Собрать корневой DI-контейнер приложения (composition root).
        /// </summary>
        private static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Регистрация сервисов слоя Infrastructure (доступ к активам).
            services.AddAssetsInfrastructure();

            // Регистрация форм - AssetsForm запрашивает зависимости через конструктор.
            services.AddTransient<AssetsForm>();

            return services.BuildServiceProvider();
        }
    }
}
