using RuInTech_TEST.Database.Entities.Enums;

namespace RuInTech_TEST.Database.Entities.Assets.NonMonetary
{
    /// <summary>
    /// Сущность неденежного актива.
    /// </summary>
    public abstract class NonMonetaryAsset : Asset
    {
        /// <summary>
        /// Начальная балансовая стоимость.
        /// </summary>
        public decimal InitialBalanceCost { get; set; }

        /// <summary>
        /// Валюта начальной балансовой стоимости.
        /// </summary>
        public CurrencyType InitialBalanceCostCurrency { get; set; }

        /// <summary>
        /// Остаточная балансовая стоимость.
        /// </summary>
        public decimal ResidualBalanceCost { get; set; }

        /// <summary>
        /// Валюта остаточной балансовой стоимости.
        /// </summary>
        public CurrencyType ResidualBalanceCostCurrency { get; set; }

        /// <summary>
        /// Оценочная стоимость.
        /// </summary>
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// Валюта оценочной стоимости.
        /// </summary>
        public CurrencyType EstimatedCostCurrency { get; set; }
    }
}
