using ShopApp.Domain.Models.Shippers.ShippersBaseModel;

namespace ShopApp.Domain.Models.Shippers
{
    public record ShippersCreateModel : ShippersModel
    {
        public int creation_user { get; set; }
    }
}
