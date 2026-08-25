using RuInTech_TEST.Contract.Models.Enums;

namespace RuInTech_TEST.Infrastructure.Dtos.Assets.Monetary
{
    /// <summary>
    /// Купон / талон.
    /// </summary>
    public class СouponDto : MonetaryAssetDto
    {
        /// <summary>
        /// Вид купона/талона.
        /// </summary>
        public string Type { get; set; }
    }
}
