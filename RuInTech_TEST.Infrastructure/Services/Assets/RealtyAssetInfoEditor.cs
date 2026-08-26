using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Database;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace RuInTech_TEST.Infrastructure.Services.Assets
{
    /// <summary>
    /// Реализация контракта <see cref="IAssetsInfoEditor"/> для платежного счета.
    /// </summary>
    internal class RealtyAssetInfoEditor : IAssetsInfoEditorGeneric<Realty>
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public RealtyAssetInfoEditor(
            IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddAsset(Realty asset)
        {
            var entity = new Database.Entities.Assets.NonMonetary.Realty
            {
                Name = asset.Name,
                AssetKind = (Database.Entities.Enums.AssetKind)asset.AssetKind,
                InitialBalanceCost = asset.InitialBalanceCost.Cost,
                InitialBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.InitialBalanceCost.Currency,
                ResidualBalanceCost = asset.ResidualBalanceCost.Cost,
                ResidualBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.ResidualBalanceCost.Currency,
                EstimatedCost = asset.EstimatedCost.Cost,
                EstimatedCostCurrency = (Database.Entities.Enums.CurrencyType)asset.EstimatedCost.Currency,
                AdditionalInfo = asset.AdditionalInfo,
                InventoryNumber = asset.InventoryNumber,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Realty.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.Id;
            }
        }

        public async Task<long?> AddAsset(Asset asset)
        {
            return await AddAsset((Realty)asset);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsset(long assetId)
        {
            var entity = new Database.Entities.Assets.NonMonetary.Realty
            {
                Id = assetId,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.Realty.Attach(entity);
                context.Entry(entity).State = EntityState.Deleted;

                return await context.SaveChangesAsync() > 0;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsset(Realty asset)
        {
            if (!asset.Id.HasValue)
            {
                return false;
            }

            using (var context = _dbContextFactory.Create())
            {
                var entity = new Database.Entities.Assets.NonMonetary.Realty
                {
                    Id = asset.Id.Value,
                    Name = asset.Name,
                    InitialBalanceCost = asset.InitialBalanceCost.Cost,
                    InitialBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.InitialBalanceCost.Currency,
                    ResidualBalanceCost = asset.ResidualBalanceCost.Cost,
                    ResidualBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.ResidualBalanceCost.Currency,
                    EstimatedCost = asset.EstimatedCost.Cost,
                    EstimatedCostCurrency = (Database.Entities.Enums.CurrencyType)asset.EstimatedCost.Currency,
                    AdditionalInfo = asset.AdditionalInfo,
                    InventoryNumber = asset.InventoryNumber,
                };

                context.Realty.Attach(entity);
                context.Entry(entity).Property(x => x.Name).IsModified = true;
                context.Entry(entity).Property(x => x.InitialBalanceCost).IsModified = true;
                context.Entry(entity).Property(x => x.InitialBalanceCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.ResidualBalanceCost).IsModified = true;
                context.Entry(entity).Property(x => x.ResidualBalanceCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.EstimatedCost).IsModified = true;
                context.Entry(entity).Property(x => x.EstimatedCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.AdditionalInfo).IsModified = true;
                context.Entry(entity).Property(x => x.InventoryNumber).IsModified = true;

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsset(Asset asset)
        {
            return await UpdateAsset((Realty)asset);
        }
    }
}
