namespace RuInTech_TEST.Database.Entities.Organization
{
    /// <summary>
    /// Сущность банка.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Идентификатор банка.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование банка.
        /// </summary>
        public string Name { get; set; }
    }
}
