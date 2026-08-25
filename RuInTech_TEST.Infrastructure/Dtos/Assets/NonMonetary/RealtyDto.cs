namespace RuInTech_TEST.Infrastructure.Dtos.Assets.NonMonetary
{
    /// <summary>
    /// Недвижимость.
    /// </summary>
    public class RealtyDto : NonMonetaryAssetDto
    {
        /// <summary>
        /// Инвентарный номер.
        /// </summary>
        public string InventoryNumber { get; set; }

        /// <summary>
        /// Дополнительная информация / примечание.
        /// </summary>
        public string AdditionalInfo { get; set; }
    }
}
