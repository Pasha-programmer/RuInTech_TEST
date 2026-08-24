namespace RuInTech_TEST.Database.Entities.Assets.Monetary
{
    /// <summary>
    /// Сущность платежного счета - актив денежных средств, расположенных в банковском счете.
    /// </summary>
    public class PaymentAccount : MonetaryAsset
    {
        /// <summary>
        /// Идентификатор банковского счета, к которому привязаны деньги.
        /// </summary>
        public long BankAccountId { get; set; }
    }
}
