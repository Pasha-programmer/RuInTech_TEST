using RuInTech_TEST.Contract.Models.Organization;
using RuInTech_TEST.Contract.Models.RawMaterial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.RawMaterialKinds
{
    /// <summary>
    /// Контракт получения информации о сырье.
    /// </summary>
    public interface IRawMaterialKindGetterService
    {
        /// <summary>
        /// Получить список сырья.
        /// </summary>
        /// <returns>Список сырья.</returns>
        Task<IReadOnlyCollection<RawMaterialKind>> GetRawMaterialKinds();
    }
}
