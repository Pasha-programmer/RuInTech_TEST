using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Активы в наличном виде (бумажные денежные средства).
    /// </summary>
    public class CashAsset : MonetaryAsset
    {
        /// <inheritdoc/>
        public override AssetKind AssetKind => AssetKind.Cash;

        /// <inheritdoc/>
        public override string Summary => $"{MonetaryValue}";
    }
}
