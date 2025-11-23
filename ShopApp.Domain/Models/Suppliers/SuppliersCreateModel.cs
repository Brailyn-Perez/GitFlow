

using ShopApp.Domain.Models.Suppliers.SuppliersBaseModel;

namespace ShopApp.Domain.Models.Suppliers
{
    public record SuppliersCreateModel : SuppliersModel
    {
        public int creartion_user { get; set; }
    }
}
