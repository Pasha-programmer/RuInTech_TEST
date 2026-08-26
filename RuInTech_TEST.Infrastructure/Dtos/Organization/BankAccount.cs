namespace RuInTech_TEST.Infrastructure.Dtos.Organization
{
    /// <summary>
    /// Банковский счет.
    /// </summary>
    public class BankAccountDto
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
        public BankDto Bank { get; set; }
    }
}
