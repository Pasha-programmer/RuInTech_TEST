using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Infrastructure.Services.Assets;

namespace RuInTech_TEST.Infrastructure
{
    /// <summary>
    /// Регистрация сервисов слоя Infrastructure в контейнере DI.
    /// </summary>
    public static class Dependencyinjection
    {
        /// <summary>
        /// Зарегистрировать сервисы работы с активами.
        /// </summary>
        public static IServiceCollection AddAssetsInfrastructure(this IServiceCollection services)
        {
            // Реализации внутренние (internal) - регистрируем их здесь,
            // внутри той же сборки, чтобы не расширять публичный контракт сборки лишними типами.
            services.AddSingleton<IAssetsInfoGetter, AssetsInfoGetter>();
            services.AddSingleton<IAssetsInfoEditor, AssetsInfoEditor>();

            return services;
        }
    }
}
