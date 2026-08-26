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
    internal class RawMaterialAssetInfoEditor : IAssetsInfoEditorGeneric<RawMaterial>
    {
        private readonly IDbContextFactory<AssetContext> _dbContextFactory;

        public RawMaterialAssetInfoEditor(
            IDbContextFactory<AssetContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public async Task<long?> AddAsset(RawMaterial asset)
        {
            var entity = new Database.Entities.Assets.NonMonetary.RawMaterial
            {
                Name = asset.Name,
                AssetKind = (Database.Entities.Enums.AssetKind)asset.AssetKind,
                InitialBalanceCost = asset.InitialBalanceCost.Cost,
                InitialBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.InitialBalanceCost.Currency,
                ResidualBalanceCost = asset.ResidualBalanceCost.Cost,
                ResidualBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.ResidualBalanceCost.Currency,
                EstimatedCost = asset.EstimatedCost.Cost,
                EstimatedCostCurrency = (Database.Entities.Enums.CurrencyType)asset.EstimatedCost.Currency,
                RawMaterialKindId = asset.RawMaterialKind.Id,
                AdditionalInfo = asset.AdditionalInfo,
                ProductionDate = asset.ProductionDate,
                Quantity = asset.Quantity,
                UnitOfMeasure = (Database.Entities.Enums.UnitOfMeasure)asset.UnitOfMeasure,
            };

            using (var context = _dbContextFactory.Create())
            {
                context.RawMaterials.Add(entity);
                if (await context.SaveChangesAsync() == 0)
                {
                    return null;
                }
                return entity.Id;
            }
        }

        public async Task<long?> AddAsset(Asset asset)
        {
            return await AddAsset((RawMaterial)asset);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsset(long assetId)
        {
            var entity = new Database.Entities.Assets.NonMonetary.RawMaterial
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
        public async Task<bool> UpdateAsset(RawMaterial asset)
        {
            if (!asset.Id.HasValue)
            {
                return false;
            }

            using (var context = _dbContextFactory.Create())
            {
                var entity = new Database.Entities.Assets.NonMonetary.RawMaterial
                {
                    Id = asset.Id.Value,
                    Name = asset.Name,
                    InitialBalanceCost = asset.InitialBalanceCost.Cost,
                    InitialBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.InitialBalanceCost.Currency,
                    ResidualBalanceCost = asset.ResidualBalanceCost.Cost,
                    ResidualBalanceCostCurrency = (Database.Entities.Enums.CurrencyType)asset.ResidualBalanceCost.Currency,
                    EstimatedCost = asset.EstimatedCost.Cost,
                    EstimatedCostCurrency = (Database.Entities.Enums.CurrencyType)asset.EstimatedCost.Currency,
                    RawMaterialKindId = asset.RawMaterialKind.Id,
                    AdditionalInfo = asset.AdditionalInfo,
                    ProductionDate = asset.ProductionDate,
                    Quantity = asset.Quantity,
                    UnitOfMeasure = (Database.Entities.Enums.UnitOfMeasure)asset.UnitOfMeasure,
                };

                context.RawMaterials.Attach(entity);
                context.Entry(entity).Property(x => x.Name).IsModified = true;
                context.Entry(entity).Property(x => x.InitialBalanceCost).IsModified = true;
                context.Entry(entity).Property(x => x.InitialBalanceCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.ResidualBalanceCost).IsModified = true;
                context.Entry(entity).Property(x => x.ResidualBalanceCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.EstimatedCost).IsModified = true;
                context.Entry(entity).Property(x => x.EstimatedCostCurrency).IsModified = true;
                context.Entry(entity).Property(x => x.RawMaterialKindId).IsModified = true;
                context.Entry(entity).Property(x => x.AdditionalInfo).IsModified = true;
                context.Entry(entity).Property(x => x.ProductionDate).IsModified = true;
                context.Entry(entity).Property(x => x.Quantity).IsModified = true;
                context.Entry(entity).Property(x => x.UnitOfMeasure).IsModified = true;

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsset(Asset asset)
        {
            return await UpdateAsset((RawMaterial)asset);
        }
    }
}
