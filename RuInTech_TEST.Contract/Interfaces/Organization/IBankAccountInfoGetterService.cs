using RuInTech_TEST.Contract.Models.Organization;
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
    }
}
