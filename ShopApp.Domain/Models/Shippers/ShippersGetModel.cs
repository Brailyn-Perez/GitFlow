

using ShopApp.Domain.Models.Shippers.ShippersBaseModel;

namespace ShopApp.Domain.Models.Shippers
{
    public record ShippersGetModel : ShippersModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
 