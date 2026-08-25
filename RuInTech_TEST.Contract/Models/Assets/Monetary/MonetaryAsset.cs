namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Денежный актив.
    /// </summary>
    public abstract class MonetaryAsset : Asset
    {
        /// <summary>
        /// Денежная стоимость.
        /// </summary>
        public MonetaryValue MonetaryValue { get; set; }
    }
}
