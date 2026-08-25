using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.FilterParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Assets
{
    /// <summary>
    /// Котракт на получение информации о активах.
    /// </summary>
    public interface IAssetsInfoGetter
    {
        /// <summary>
        /// Получить активы.
        /// </summary>
        /// <returns>Коллекция активов.</returns>
        Task<IReadOnlyCollection<Asset>> GetAssets(AssetFilterParameters assetFilterParameters);

        /// <summary>
        /// Получить актив по идентификатру.
        /// </summary>
        /// <param name="id">Идентификатор актива.</param>
        /// <returns>Актив.</returns>
        Task<Asset> GetAsset(long id);
    }
}
