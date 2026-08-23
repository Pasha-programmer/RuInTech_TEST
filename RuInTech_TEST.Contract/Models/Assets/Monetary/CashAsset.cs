using RuInTech_TEST.Contract.Models.Enums;
using System;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Активы в наличном виде (бумажные денежные средства).
    /// </summary>
    public class CashAsset : MonetaryAsset
    {
        public CashAsset(
            long? id,
            string name,
            MonetaryValue monetaryValue) 
            : base(id, name, monetaryValue)
        { }

        /// <inheritdoc/>
        public override AssetKind DisplayTypeName => AssetKind.Cash;

        /// <inheritdoc/>
        public override string Summary => $"{MonetaryValue}";
    }
}
