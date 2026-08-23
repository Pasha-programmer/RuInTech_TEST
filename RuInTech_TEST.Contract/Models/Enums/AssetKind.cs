using System.ComponentModel;

namespace RuInTech_TEST.Contract.Models.Enums
{
    /// <summary>
    /// Виды активов.
    /// </summary>
    public enum AssetKind
    {
        [Description("Наличные")]
        Cash = 1,

        [Description("Платежный счет")]
        PaymentAccount = 2,

        [Description("Талон / купон")]
        Coupon = 3,

        [Description("Сырьё / материалы")]
        RawMaterial = 4,

        [Description("Недвижимость")]
        Realty = 5,
    }
}
