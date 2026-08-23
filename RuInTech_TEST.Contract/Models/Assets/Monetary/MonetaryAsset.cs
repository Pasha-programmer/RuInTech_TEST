using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Денежный актив.
    /// </summary>
    public abstract class MonetaryAsset : Asset
    {
        protected MonetaryAsset(
            long? id,
            string name,
            MonetaryValue monetaryValue) 
            : base(id, name)
        {
            MonetaryValue = monetaryValue;
        }

        /// <summary>
        /// Денежная стоимость.
        /// </summary>
        public MonetaryValue MonetaryValue { get; set; }
    }
}
