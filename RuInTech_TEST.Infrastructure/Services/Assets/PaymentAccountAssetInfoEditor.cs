using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Assets
{
    /// <summary>
    /// Реализация контракта <see cref="IAssetsInfoEditor"/> для платежного счета.
    /// </summary>
    internal class PaymentAccountAssetInfoEditor : IAssetsInfoEditor<PaymentAccount>
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public PaymentAccountAssetInfoEditor(
            IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddAsset(PaymentAccount asset)
        {
            var entity = new Database.Entities.Assets.Monetary.PaymentAccount
            {
                Name = asset.Name,
                AssetKind = (Database.Entities.Enums.AssetKind)asset.AssetKind,
                Cost = asset.MonetaryValue.Cost,
                Currency = (Database.Entities.Enums.CurrencyType)asset.MonetaryValue.Currency,
                BankAccountId = asset.BankAccount.Id,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.PaymentAccount.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.Id;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsset(long assetId)
        {
            var entity = new Database.Entities.Assets.Monetary.PaymentAccount
            {
                Id = assetId,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Entry(entity).State = EntityState.Deleted;

                return await context.SaveChangesAsync() > 0;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsset(PaymentAccount asset)
        {
            if (!asset.Id.HasValue)
            {
                return false;
            }

            using (var context = _dbContextFactory.Create())
            {
                var entity = new Database.Entities.Assets.Monetary.PaymentAccount
                {
                    Id = asset.Id.Value,
                    Name = asset.Name,
                    Cost = asset.MonetaryValue.Cost,
                    Currency = (Database.Entities.Enums.CurrencyType)asset.MonetaryValue.Currency,
                    BankAccountId = asset.BankAccount.Id,
                };

                context.Entry(entity).Property(x => x.Name).IsModified = true;
                context.Entry(entity).Property(x => x.Cost).IsModified = true;
                context.Entry(entity).Property(x => x.Currency).IsModified = true;
                context.Entry(entity).Property(x => x.BankAccountId).IsModified = true;

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
