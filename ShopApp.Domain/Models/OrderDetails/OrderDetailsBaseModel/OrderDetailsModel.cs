

namespace ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel
{
    public record OrderDetailsModel
    {
        public int orderid { get; set; }
        public int productid { get; set; }
        public decimal unitprice { get; set; }
        public decimal qty { get; set; }
        public decimal discount { get; set; }
    }
}
