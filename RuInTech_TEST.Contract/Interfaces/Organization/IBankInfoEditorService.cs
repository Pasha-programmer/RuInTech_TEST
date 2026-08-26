using RuInTech_TEST.Contract.Models.Organization;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Organization
{
    /// <summary>
    /// Контракт редактирования информации о банке.
    /// </summary>
    public interface IBankInfoEditorService
    {
        /// <summary>
        /// Добавить банк.
        /// </summary>
        /// <param name="bank">Модель банка.</param>
        /// <returns>true - если добавление успешно сохранено, иначе - false.</returns>
        Task<long?> AddBank(Bank bank);

        /// <summary>
        /// Удалить банк.
        /// </summary>
        /// <param name="bankId">Идентификатор банка.</param>
        /// <returns>true - если удаление успешно, иначе - false.</returns>
        Task<bool> DeleteBank(long bankId);
    }
}
