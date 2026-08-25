using Microsoft.Extensions.DependencyInjection;
using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Database;
using RuInTech_TEST.Infrastructure.Dtos.Assets.Monetary;
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
            services.AddAssetsDatebase();

            services.AddSingleton<IAssetsInfoGetter, AssetsInfoGetter>();
            services.AddSingleton<IAssetsInfoEditorGeneric<CashAsset>, CashAssetInfoEditor>();
            services.AddSingleton<IAssetsInfoEditorGeneric<PaymentAccount>, PaymentAccountAssetInfoEditor>();
            services.AddSingleton<IAssetsInfoEditorGeneric<Сoupon>, СouponAssetInfoEditor>();
            services.AddSingleton<IAssetsInfoEditorGeneric<RawMaterial>, RawMaterialAssetInfoEditor>();
            services.AddSingleton<IAssetsInfoEditorGeneric<Realty>, RealtyAssetInfoEditor>();

            return services;
        }
    }
}
