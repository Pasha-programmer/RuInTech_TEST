namespace RuInTech_TEST.Database.Entities.Assets.NonMonetary
{
    /// <summary>
    /// Сущность недвижимости.
    /// </summary>
    public class Realty : NonMonetaryAsset
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
