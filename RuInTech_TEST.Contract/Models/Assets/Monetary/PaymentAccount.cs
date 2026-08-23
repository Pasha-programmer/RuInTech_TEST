using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.Organization;
using System;

namespace RuInTech_TEST.Contract.Models.Assets.Monetary
{
    /// <summary>
    /// Платежный счет - актив денежных средств, расположенных в банковском счете.
    /// </summary>
    public class PaymentAccount : MonetaryAsset
    {
        public PaymentAccount(
            long? id,
            string name,
            MonetaryValue monetaryValue, 
            BankAccount bankAccount) 
            : base(id, name, monetaryValue)
        {
            BankAccount = bankAccount;
        }

        /// <summary>
        /// Банковский счет, к которому привязаны деньги.
        /// </summary>
        public BankAccount BankAccount { get; set; }

        /// <inheritdoc/>
        public override AssetKind DisplayTypeName => AssetKind.PaymentAccount;

        /// <inheritdoc/>
        public override string Summary => $"{MonetaryValue}; банк: {BankAccount.Bank.Name}; счёт № {BankAccount.PersonalAccount}";
    }
}
