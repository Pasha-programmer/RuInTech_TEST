namespace RuInTech_TEST.Database.Entities.Enums
{
    /// <summary>
    /// Виды активов.
    /// </summary>
    public enum AssetKind
    {
        /// <summary>
        /// Наличные.
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Платежный счет.
        /// </summary>
        PaymentAccount = 2,

        /// <summary>
        /// Талон / купон.
        /// </summary>
        Coupon = 3,

        /// <summary>
        /// Сырьё / материалы.
        /// </summary>
        RawMaterial = 4,

        /// <summary>
        /// Недвижимость.
        /// </summary>
        Realty = 5,
    }
}
