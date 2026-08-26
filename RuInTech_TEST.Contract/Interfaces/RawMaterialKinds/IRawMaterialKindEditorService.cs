using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Contract.Models.RawMaterial;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.RawMaterialKinds
{
    /// <summary>
    /// Контракт редактирования информации о сырье.
    /// </summary>
    public interface IRawMaterialKindEditorService
    {
        /// <summary>
        /// Добавить сырье.
        /// </summary>
        /// <param name="rawMaterialKind">Модель сырья.</param>
        /// <returns>true - если добавление успешно сохранено, иначе - false.</returns>
        Task<long?> AddRawMaterialKind(RawMaterialKind rawMaterialKind);

        /// <summary>
        /// Удалить сырье.
        /// </summary>
        /// <param name="rawMaterialKindId">Идентификатор сырья.</param>
        /// <returns>true - если удаление успешно, иначе - false.</returns>
        Task<bool> DeleteRawMaterialKind(long rawMaterialKindId);
    }
}
