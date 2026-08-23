namespace RuInTech_TEST.Contract.Models.Organization
{
    /// <summary>
    /// Банк.
    /// </summary>
    public class Bank
    {
        public Bank(long id, string name)
        {
            Id = id;
            Name = name;
        }

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
