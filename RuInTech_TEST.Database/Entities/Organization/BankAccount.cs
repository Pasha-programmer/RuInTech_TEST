namespace RuInTech_TEST.Database.Entities.Organization
{
    /// <summary>
    /// Сущность банковского счета.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Идентификатор банковского счета.
        /// </summary>
        public long BankAccountId { get; set; }

        /// <summary>
        /// Лицевой счет клиента банка.
        /// </summary>
        public string PersonalAccount { get; set; }

        /// <summary>
        /// Идентификатор банка.
        /// </summary>
        public long BankId { get; set; }
    }
}
