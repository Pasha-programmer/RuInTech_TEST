using RuInTech_TEST.Database.Entities.Enums;

namespace RuInTech_TEST.Database.Entities.Assets
{
    /// <summary>
    /// Сущность актива.
    /// </summary>
    public class Asset
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
