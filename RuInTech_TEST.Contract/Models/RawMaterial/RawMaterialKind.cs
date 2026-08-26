namespace RuInTech_TEST.Contract.Models.RawMaterial
{
    /// <summary>
    /// Сущность вида сырья/материала.
    /// </summary>
    public class RawMaterialKind
    {
        /// <summary>
        /// Идентификатор сырья.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Наименвоание сырья.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дополнительная информация о сырье / описание.
        /// </summary>
        public string Description { get; set; } 

    }
}
