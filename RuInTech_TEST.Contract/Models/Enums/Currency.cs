using System.ComponentModel;

namespace RuInTech_TEST.Contract.Models.Enums
{
    /// <summary>
    /// Типы валюты.
    /// </summary>
    public enum CurrencyType : int
    {
        /// <summary>
        /// Рубли.
        /// </summary>
        [Description("Рубли")]
        RUB = 1,

        /// <summary>
        /// Доллары.
        /// </summary>
        [Description("Доллары")]
        USD = 2,

        /// <summary>
        /// Евро.
        /// </summary>
        [Description("Евро")]
        EUR = 3,
    }
}
