using RuInTech_TEST.Contract.Interfaces.Assets;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.DataAccess.Data;
using System.Collections.Generic;
using System.Linq;

namespace RuInTech_TEST.Infrastructure.Services.Assets
{
    /// <summary>
    /// Реализация контракта <see cref="IAssetsInfoGetter"/>
    /// </summary>
    internal class AssetsInfoGetter : IAssetsInfoGetter
    {
        /// <inheritdoc/>
        public IReadOnlyList<Asset> GetAssets()
        {
            return SampleData.Assets.ToArray();
        }
    }
}
