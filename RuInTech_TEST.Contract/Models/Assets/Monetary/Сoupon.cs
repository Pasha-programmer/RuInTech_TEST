using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Купон / талон.
    /// </summary>
    public class Сoupon : MonetaryAsset
    {
        public Сoupon(
            long? id,
            string name,
            MonetaryValue monetaryValue,
            string type)
            : base(id, name, monetaryValue)
        {
            Type = type;
        }

        /// <summary>
        /// Вид купона/талона.
        /// </summary>
        public string Type { get; set; }

        /// <inheritdoc/>
        public override AssetKind AssetKind => AssetKind.Coupon;

        /// <inheritdoc/>
        public override string Summary => $"{MonetaryValue}; вид: {Type}";
    }
}
