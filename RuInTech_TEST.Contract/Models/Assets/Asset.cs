using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Contract.Models.Assets
{
    /// <summary>
    /// Базовое представление актива.
    /// </summary>
    public abstract class Asset
    {
        public Asset(long? id, string name)
        {
            Id = id;
            Name = name;
        }

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
        public abstract AssetKind DisplayTypeName { get; }

        /// <summary>
        /// Краткая сводка по активу для колонки списка
        /// </summary>
        public abstract string Summary { get; }
    }
}
