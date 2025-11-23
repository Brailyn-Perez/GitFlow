

using ShopApp.Domain.Models.Shippers.ShippersBaseModel;

namespace ShopApp.Domain.Models.Shippers
{
    public record ShippersDeleteModel : ShippersModel
    {
        public int delete_user { get; set; }
    }
}
