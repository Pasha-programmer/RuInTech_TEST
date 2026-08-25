namespace RuInTech_TEST.Contract.Models.Organization
{
    /// <summary>
    /// Банковский счет.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Идентификатор банковского счета.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Лицевой счет клиента банка.
        /// </summary>
        public string PersonalAccount { get; set; }

        /// <summary>
        /// Банк.
        /// </summary>
        public Bank Bank { get; set; }
    }
}
