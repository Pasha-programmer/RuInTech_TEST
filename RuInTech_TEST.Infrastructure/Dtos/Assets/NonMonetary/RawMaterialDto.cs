using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.RawMaterial;
using System;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets.NonMonetary
{
    /// <summary>
    /// Активы в виде сырья.
    /// </summary>
    public class RawMaterialDto : NonMonetaryAssetDto
    {
        /// <summary>
        /// Вид сырья.
        /// </summary>
        public RawMaterialKind RawMaterialKind { get; set; }

        /// <summary>
        /// Единицы измерения.
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; }

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
    }
}
