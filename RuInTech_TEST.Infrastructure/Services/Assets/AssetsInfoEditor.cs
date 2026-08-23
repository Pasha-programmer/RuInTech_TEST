using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.DataAccess.Data;
using System.Linq;

namespace RuInTech_TEST.Infrastructure.Services.Assets
{
    /// <summary>
    /// Реализация контракта <see cref="IAssetsInfoEditor"/>.
    /// </summary>
    internal class AssetsInfoEditor : IAssetsInfoEditor
    {
        /// <inheritdoc/>
        public long? AddAsset(Asset asset)
        {
            var maxId = SampleData.Assets.Count > 0
                ? SampleData.Assets.Select(a => a.Id.Value).Max()
                : default;
            asset.Id = maxId + 1;

            SampleData.Assets.Add(asset);

            return asset.Id;
        }

        /// <inheritdoc/>
        public bool DeleteAsset(long assetId)
        {
            var assetToDelete = SampleData.Assets.FirstOrDefault(a => a.Id == assetId);

            if (assetToDelete == null)
            {
                return true;
            }

            return SampleData.Assets.Remove(assetToDelete);
        }

        /// <inheritdoc/>
        public bool UpdateAsset(Asset asset)
        {
            var assetToUpdate = SampleData.Assets.FirstOrDefault(a => a.Id == asset.Id);

            if (assetToUpdate == null)
            {
                return false;
            }

            var index = SampleData.Assets.IndexOf(assetToUpdate);
            SampleData.Assets[index] = asset;

            assetToUpdate = asset;

            return true;
        }
    }
}
