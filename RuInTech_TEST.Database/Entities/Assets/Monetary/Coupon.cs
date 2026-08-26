namespace RuInTech_TEST.Database.Entities.Assets.Monetary
{
    /// <summary>
    /// Сущность купона / талона.
    /// </summary>
    public class Coupon : MonetaryAsset
    {
        /// <summary>
        /// Вид купона/талона.
        /// </summary>
        public string Type { get; set; }
    }
}
