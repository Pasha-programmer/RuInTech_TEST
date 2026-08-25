using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.FilterParameters
{
    /// <summary>
    /// Параметры фильтрации актива.
    /// </summary>
    public class AssetFilterParameters
    {
        /// <summary>
        /// Идентификатор актива.
        /// </summary>
        public long[] AssetIds { get; set; }

        /// <summary>
        /// Наименование актива.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип актива.
        /// </summary>
        public AssetKind[] AssetKinds { get; }
    }
}
