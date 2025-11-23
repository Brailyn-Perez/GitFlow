

using ShopApp.Domain.Models.Customers.CustomersBaseModel;

namespace ShopApp.Domain.Models.Customers
{
    public record CustomersDeleteModel : CustomersModel
    {
        public int delete_user { get; set; }
    }
}
