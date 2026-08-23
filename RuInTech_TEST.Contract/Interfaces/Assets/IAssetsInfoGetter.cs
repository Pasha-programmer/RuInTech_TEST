using RuInTech_TEST.Contract.Models.Assets;
using System.Collections.Generic;

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
        IReadOnlyList<Asset> GetAssets();
    }
}
