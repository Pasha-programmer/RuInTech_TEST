using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets
{
    /// <summary>
    /// Базовое представление актива.
    /// </summary>
    public class AssetDto
    {
        /// <summary>
        /// Идентификатор актива.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Наименование актива.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип актива.
        /// </summary>
        public AssetKind AssetKind { get; set; }
    }
}
