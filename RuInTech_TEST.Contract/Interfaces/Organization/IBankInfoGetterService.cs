using RuInTech_TEST.Contract.Models.Organization;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Organization
{
    /// <summary>
    /// Контракт получения информации о банке.
    /// </summary>
    public interface IBankInfoGetterService
    {
        /// <summary>
        /// Получить полную информацию о банке.
        /// </summary>
        /// <param name="bankId">Идентификатор банка.</param>
        /// <returns>Модель информации о банке.</returns>
        Task<Bank> GetBankFullInfo(long bankId);
    }
}
