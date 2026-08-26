using RuInTech_TEST.Contract.Models.Organization;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Organization
{
    /// <summary>
    /// Контракт редактирования информации о банковском счете.
    /// </summary>
    public interface IBankAccountInfoEditorService
    {
        /// <summary>
        /// Добавить банковский счет.
        /// </summary>
        /// <returns></returns>
        Task<long?> AddBankAccount(BankAccount bankAccount);

        /// <summary>
        /// Удалить банковский счет.
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteBankAccount(long bankAccountId);
    }
}
