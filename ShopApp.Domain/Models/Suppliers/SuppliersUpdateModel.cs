
using ShopApp.Domain.Models.Suppliers.SuppliersBaseModel;

namespace ShopApp.Domain.Models.Suppliers
{
    public record SuppliersUpdateModel : SuppliersModel
    {
        public int modify_user { get; set; }
    }
}
