namespace RuInTech_TEST.Contract.Models.Assets.NonMonetary
{
    /// <summary>
    /// Неденежный актив.
    /// </summary>
    public abstract class NonMonetaryAsset : Asset
    {
        protected NonMonetaryAsset(
            long? id,
            string name,
            MonetaryValue initialBalanceCost,
            MonetaryValue residualBalanceCost,
            MonetaryValue estimatedCost) 
            : base(id, name)
        {
            InitialBalanceCost = initialBalanceCost;
            ResidualBalanceCost = residualBalanceCost;
            EstimatedCost = estimatedCost;
        }

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
