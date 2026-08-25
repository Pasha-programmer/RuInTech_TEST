using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.Organization;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Платежный счет - актив денежных средств, расположенных в банковском счете.
    /// </summary>
    public class PaymentAccount : MonetaryAsset
    {
        /// <summary>
        /// Банковский счет, к которому привязаны деньги.
        /// </summary>
        public BankAccount BankAccount { get; set; }

        /// <inheritdoc/>
        public override AssetKind AssetKind => AssetKind.PaymentAccount;

        /// <inheritdoc/>
        public override string Summary => $"{MonetaryValue}; банк: {BankAccount.Bank.Name}; счёт № {BankAccount.PersonalAccount}";
    }
}
