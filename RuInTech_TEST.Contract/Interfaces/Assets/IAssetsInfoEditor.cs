using RuInTech_TEST.Contract.Models.Assets;
using System.Threading.Tasks;

namespace RuInTech_TEST.Contract.Interfaces.Assets
{
    /// <summary>
    /// Котракт на получение информации о активах.
    /// </summary>
    public interface IAssetsInfoEditor<T> where T : Asset
    {
        /// <summary>
        /// Изменить информацию о активе.
        /// </summary>
        /// <param name="asset">Обновляемый актив.</param>
        /// <returns>true - если обновление успешно, иначе false.</returns>
        Task<bool> UpdateAsset(T asset);

        /// <summary>
        /// Добавить новый актив.
        /// </summary>
        /// <param name="asset">Новый актив.</param>
        /// <returns>Идентификатор актива - если добавление успешно, иначе null.</returns>
        Task<long?> AddAsset(T asset);

        /// <summary>
        /// Удалить актив.
        /// </summary>
        /// <param name="assetId">Идентификатор актива.</param>
        /// <returns>true - если добавление успешно, иначе false.</returns>
        Task<bool> DeleteAsset(long assetId);
    }
}
