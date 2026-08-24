using RuInTech_TEST.Database.Entities.Enums;

namespace RuInTech_TEST.Database.Entities.Assets.Monetary
{
    /// <summary>
    /// Сущность денежного актива.
    /// </summary>
    public class MonetaryAsset : Asset
    {
        /// <summary>
        /// Стоимость.
        /// </summary>
        public decimal Cost { get; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType Currency { get; }
    }
}
