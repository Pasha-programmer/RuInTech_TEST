using Microsoft.Extensions.DependencyInjection;

namespace RuInTech_TEST.UI
{
    /// <summary>
    /// Регистрация сервисов слоя UI в контейнере DI.
    /// </summary>
    public static class Dependencyinjection
    {
        /// <summary>
        /// Зарегистрировать сервисы работы с активами.
        /// </summary>
        public static IServiceCollection AddUIForms(this IServiceCollection services)
        {
            services.AddTransient<AssetsForm>();
            services.AddTransient<AssetEditForm>();

            return services;
        }
    }
}
