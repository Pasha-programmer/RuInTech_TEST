using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets.NonMonetary
{
    /// <summary>
    /// Неденежный актив.
    /// </summary>
    public class NonMonetaryAssetDto : AssetDto
    {
        /// <summary>
        /// Начальная балансовая стоимость.
        /// </summary>
        public decimal InitialBalanceCost { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType InitialBalanceCostCurrency { get; set; }

        /// <summary>
        /// Остаточная балансовая стоимость.
        /// </summary>
        public decimal ResidualBalanceCost { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType ResidualBalanceCostCurrency { get; set; }

        /// <summary>
        /// Оценочная стоимость.
        /// </summary>
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType EstimatedCostCurrency { get; set; }
    }
}
