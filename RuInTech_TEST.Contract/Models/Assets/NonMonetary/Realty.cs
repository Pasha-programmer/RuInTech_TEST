using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.Assets.NonMonetary
{
    /// <summary>
    /// Недвижимость.
    /// </summary>
    public class Realty : NonMonetaryAsset
    {
        public Realty(
            long? id,
            string name,
            MonetaryValue initialBalanceCost,
            MonetaryValue residualBalanceCost,
            MonetaryValue estimatedCost,
            string inventoryNumber,
            string additionalInfo)
            : base(id, name, initialBalanceCost, residualBalanceCost, estimatedCost)
        {
            InventoryNumber = inventoryNumber;
            AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Инвентарный номер.
        /// </summary>
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Дополнительная информация / примечание.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <inheritdoc/>
        public override AssetKind AssetKind => AssetKind.Realty;

        /// <inheritdoc/>
        public override string Summary => $"Баланс: {InitialBalanceCost} - {ResidualBalanceCost}; " +
                $"оценка: {EstimatedCost}; инв. № {InventoryNumber}";
    }
}
