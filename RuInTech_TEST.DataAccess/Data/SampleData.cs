using RuInTech_TEST.Contract.Models;
using RuInTech_TEST.Contract.Models.Assets;
using RuInTech_TEST.Contract.Models.Assets.Monetary;
using RuInTech_TEST.Contract.Models.Assets.NonMonetary;
using RuInTech_TEST.Contract.Models.Enums;
using RuInTech_TEST.Contract.Models.Organization;
using System;
using System.Collections.Generic;

//TODO: убрать ссылку из этого проекта на проект контракта, 
//тут добавить свои модели данных
//(для БД тут будут сущности, которые будут мапиться в dto в проекте инфрастуктуры)
namespace RuInTech_TEST.DataAccess.Data
{
    /// <summary>
    /// Класс для инициализации тестовыми данными из задания.
    /// </summary>
    public static class SampleData
    {
        private readonly static IList<Bank> Banks = new List<Bank>
        {
            new Bank(1, "ЕвроВорБанк"),
            new Bank(2, "Внешторгабк"),
        };

        /// <summary>
        /// Возвращает список тестовых активов.
        /// </summary>
        public readonly static IList<Asset> Assets = new List<Asset>
        {
            // 1. Деньги на счету в банке (рубли)
            new PaymentAccount(
                1,
                "Счет в ЕвроВорБанке",
                new MonetaryValue(1000, CurrencyType.RUB),
                new BankAccount(
                    "5",
                    Banks[0]
                )
            ),

            // 2. Деньги на счету в банке (доллары)
            new PaymentAccount(
                2,
                "Счет во Внешторгабке",
                new MonetaryValue(5, CurrencyType.USD),
                new BankAccount(
                    "3",
                    Banks[1]
                )
            ),

            // 3. Деньги в кассе
            new CashAsset(
                3,
                "Наличные в кассе",
                new MonetaryValue(100, CurrencyType.RUB)
            ),

            // 4. Талон на бензин (тоже денежный актив)
            new Сoupon(
                4,
                "Талон на бензин от Аспека",
                new MonetaryValue(3000, CurrencyType.RUB),
                "Талон на бензин"
            ),

            // 5. Неденежный актив - здание
            new Realty(
                5,
                "Торговое здание по адресу Бассейная-6",
                new MonetaryValue(30000, CurrencyType.RUB),
                new MonetaryValue(5000, CurrencyType.RUB),
                new MonetaryValue(1000000, CurrencyType.RUB),
                "7",
                "Год постройки: 1970, адрес: Бассейная-6"
            ),

            // 6. Неденежный актив - гвозди
            new RawMaterial(
                6,
                "Гвозди строительные",
                new MonetaryValue(1000, CurrencyType.RUB),
                new MonetaryValue(100, CurrencyType.RUB),
                new MonetaryValue(2000, CurrencyType.RUB),
                "Гвозди",
                "кг",
                100,
                new DateTimeOffset(new DateTime(2000, 1, 1))
            ),
        };
    }
}
