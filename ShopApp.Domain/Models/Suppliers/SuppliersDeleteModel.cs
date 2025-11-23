

using ShopApp.Domain.Models.Suppliers.SuppliersBaseModel;

namespace ShopApp.Domain.Models.Suppliers
{
    public record SuppliersDeleteModel : SuppliersModel
    {
        public int delete_user { get; set; }
    }
}
