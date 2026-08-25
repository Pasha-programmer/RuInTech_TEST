using System.ComponentModel;

namespace RuInTech_TEST.Contract.Models.Enums
{
    /// <summary>
    /// Единицы измерения.
    /// </summary>
    public enum UnitOfMeasure
    {
        /// <summary>
        /// Килограмм.
        /// </summary>
        [Description("кг")]
        Kilogram = 1,

        /// <summary>
        /// Литр.
        /// </summary>
        [Description("л")]
        Liter = 2,

        /// <summary>
        /// Штука.
        /// </summary>
        [Description("шт")]
        Piece = 3
    }
}
