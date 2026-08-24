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
        public decimal InitialBalanceCost { get; }

        /// <summary>
        /// Валюта начальной балансовой стоимости.
        /// </summary>
        public CurrencyType InitialBalanceCostCurrency { get; }

        /// <summary>
        /// Остаточная балансовая стоимость.
        /// </summary>
        public decimal ResidualBalanceCost { get; }

        /// <summary>
        /// Валюта остаточной балансовой стоимости.
        /// </summary>
        public CurrencyType ResidualBalanceCostCurrency { get; }

        /// <summary>
        /// Оценочная стоимость.
        /// </summary>
        public decimal EstimatedCost { get; }

        /// <summary>
        /// Валюта оценочной стоимости.
        /// </summary>
        public CurrencyType EstimatedCostCurrency { get; }
    }
}
