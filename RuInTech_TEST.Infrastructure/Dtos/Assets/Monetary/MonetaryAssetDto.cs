using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets.Monetary
{
    /// <summary>
    /// Денежный актив.
    /// </summary>
    public class MonetaryAssetDto : AssetDto
    {
        /// <summary>
        /// Денежная стоимость.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType Currency { get; set; }
    }
}
