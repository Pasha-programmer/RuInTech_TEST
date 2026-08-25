using RuInTech_TEST.Contract.Models.Organization;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets.Monetary
{
    /// <summary>
    /// Платежный счет - актив денежных средств, расположенных в банковском счете.
    /// </summary>
    public class PaymentAccountDto : MonetaryAssetDto
    {
        /// <summary>
        /// Банковский счет, к которому привязаны деньги.
        /// </summary>
        public BankAccount BankAccount { get; set; }
    }
}
