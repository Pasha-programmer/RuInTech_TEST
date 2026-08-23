using RuInTech_TEST.Contract.Models.Enums;
using System;
using System.Globalization;

namespace RuInTech_TEST.Contract.Models.Assets.NonMonetary
{
    /// <summary>
    /// Активы в виде сырья.
    /// </summary>
    public class RawMaterial : NonMonetaryAsset
    {
        public RawMaterial(
            long? id,
            string name,
            MonetaryValue initialBalanceCost,
            MonetaryValue residualBalanceCost,
            MonetaryValue estimatedCost,
            string type,
            string unitOfMeasure,
            double quantity,
            DateTimeOffset?
            productionDate,
            string additionalInfo = null)
            : base(id, name, initialBalanceCost, residualBalanceCost, estimatedCost)
        {
            Type = type;
            UnitOfMeasure = unitOfMeasure;
            Quantity = quantity;
            ProductionDate = productionDate;
            AdditionalInfo = additionalInfo;
        }

        //TODO: привести в enum
        /// <summary>
        /// Вид сырья.
        /// </summary>
        public string Type { get; set; }

        //TODO: привести в enum
        /// <summary>
        /// Единицы измерения.
        /// </summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Дата производства.
        /// </summary>
        public DateTimeOffset? ProductionDate { get; set; }

        /// <summary>
        /// Дополнительная информация / примечание.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <inheritdoc/>
        public override AssetKind AssetKind => AssetKind.RawMaterial;

        /// <inheritdoc/>
        public override string Summary => $"{Quantity.ToString(CultureInfo.InvariantCulture)} {UnitOfMeasure} ({Type}); " +
                $"баланс: {InitialBalanceCost} - {ResidualBalanceCost}; " +
                $"оценка: {EstimatedCost}" +
                (ProductionDate.HasValue ? $"; год: {ProductionDate.Value.Year}" : string.Empty);
    }
}
