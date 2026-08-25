namespace RuInTech_TEST.Contract.Models.Assets.NonMonetary
{
    /// <summary>
    /// Неденежный актив.
    /// </summary>
    public abstract class NonMonetaryAsset : Asset
    {
        /// <summary>
        /// Начальная балансовая стоимость.
        /// </summary>
        public MonetaryValue InitialBalanceCost { get; set; }

        /// <summary>
        /// Остаточная балансовая стоимость.
        /// </summary>
        public MonetaryValue ResidualBalanceCost { get; set; }

        /// <summary>
        /// Оценочная стоимость.
        /// </summary>
        public MonetaryValue EstimatedCost { get; set; }
    }
}
