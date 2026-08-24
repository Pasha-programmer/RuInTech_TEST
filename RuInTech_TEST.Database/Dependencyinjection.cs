using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Database.Context;
using System.Data.Entity.Infrastructure;

namespace RuInTech_TEST.Database
{
    /// <summary>
    /// Регистрация сервисов слоя Database в контейнере DI.
    /// </summary>
    public static class Dependencyinjection
    {
        /// <summary>
        /// Зарегистрировать провайдер базы данных.
        /// </summary>
        public static IServiceCollection AddAssetsDatebase(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory<AssetContext>>(provider => new AssetContextFactory("Assets"));

            return services;
        }
    }
}
