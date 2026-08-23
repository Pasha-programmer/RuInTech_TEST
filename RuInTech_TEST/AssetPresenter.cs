using RuInTech_TEST.Common.Extensions;
using RuInTech_TEST.Contract.Models.Assets;

namespace RuInTech_TEST
{
    /// <summary>
    /// Строковые представления активов для отображения в списке.
    /// </summary>
    internal static class AssetPresenter
    {
        /// <summary>
        /// Название типа актива.
        /// </summary>
        public static string GetTypeName(Asset asset)
        {
            return asset.DisplayTypeName.GetDescription();
        }

        /// <summary>
        /// Краткая сводка по активу для колонки списка.
        /// </summary>
        public static string GetSummary(Asset asset)
        {
            return asset.Summary;
        }
    }
}
