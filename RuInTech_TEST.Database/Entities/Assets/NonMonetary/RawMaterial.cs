using RuInTech_TEST.Database.Entities.Enums;
using System;

namespace RuInTech_TEST.Database.Entities.Assets.NonMonetary
{
    /// <summary>
    /// Сущность актива в виде сырья.
    /// </summary>
    public class RawMaterial : NonMonetaryAsset
    {
        /// <summary>
        /// Идентификатор вида сырья.
        /// </summary>
        public long RawMaterialKindId { get; set; }

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
