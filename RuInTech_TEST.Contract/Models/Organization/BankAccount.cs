namespace RuInTech_TEST.Contract.Models.Organization
{
    /// <summary>
    /// Банковский счет.
    /// </summary>
    public class BankAccount
    {
        public BankAccount(string personalAccount, Bank bank)
        {
            PersonalAccount = personalAccount;
            Bank = bank;
        }

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
