

using ShopApp.Domain.Models.Suppliers.SuppliersBaseModel;

namespace ShopApp.Domain.Models.Suppliers
{
    public record SuppliersGetModel : SuppliersModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
