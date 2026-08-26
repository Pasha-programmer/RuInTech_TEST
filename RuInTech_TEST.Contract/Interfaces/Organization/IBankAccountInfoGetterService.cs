using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Organization
{
    /// <summary>
    /// Контракт получения информации о банковском счете.
    /// </summary>
    public interface IBankAccountInfoGetterService
    {
        /// <summary>
        /// Получить лицевой счет.
        /// </summary>
        /// <returns>Лицевой счет в банке.</returns>
        Task<string> GetPersonalAccount(long bankAccountId);

        /// <summary>
        /// Проверить идентификатор лицевого счета в опеределенном банке.
        /// </summary>
        /// <param name="personalAccount">Лицевой счет.</param>
        /// <param name="bankId">Идентификатор банка.</param>
        /// <returns>Идентификатор лицевого счета.</returns>
        Task<long?> GetPersonalAccountId(string personalAccount, long bankId);
    }
}
