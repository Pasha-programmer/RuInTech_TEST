using RuInTech_TEST.Contract.Models.Enums;
using System.Globalization;

namespace RuInTech_TEST.Contract.Models
{
    public readonly struct MonetaryValue
    {
        public MonetaryValue(decimal cost, CurrencyType currency)
        {
            Cost = cost;
            Currency = currency;
        }

        /// <summary>
        /// Сумма.
        /// </summary>
        public decimal Cost { get; }

        /// <summary>
        /// Валюта.
        /// </summary>
        public CurrencyType Currency { get; }

        public override string ToString()
        {
            return $"{Cost.ToString("N2", CultureInfo.InvariantCulture)} {Currency}";
        }
    }
}
