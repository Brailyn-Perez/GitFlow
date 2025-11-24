

using ShopApp.Domain.Models.Shippers.ShippersBaseModel;

namespace ShopApp.Domain.Models.Shippers
{
    public record ShippersUpdateModel : ShippersModel
    {
        public int modify_user { get; set; }
    }
}
 