using RuInTech_TEST.Contract.Models.Assets;

namespace RuInTech_TEST.Contract.Interfaces.Assets
{
    /// <summary>
    /// Котракт на получение информации о активах.
    /// </summary>
    public interface IAssetsInfoEditor
    {
        /// <summary>
        /// Изменить информацию о активе.
        /// </summary>
        /// <param name="asset">Обновляемый актив.</param>
        /// <returns>true - если обновление успешно, иначе false.</returns>
        bool UpdateAsset(Asset asset);

        /// <summary>
        /// Добавить новый актив.
        /// </summary>
        /// <param name="asset">Новый актив.</param>
        /// <returns>Идентификатор актива - если добавление успешно, иначе null.</returns>
        long? AddAsset(Asset asset);

        /// <summary>
        /// Удалить актив.
        /// </summary>
        /// <param name="assetId">Идентификатор актива.</param>
        /// <returns>true - если добавление успешно, иначе false.</returns>
        bool DeleteAsset(long assetId);
    }
}
