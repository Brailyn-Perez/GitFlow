

using ShopApp.Domain.Models.Customers.CustomersBaseModel;

namespace ShopApp.Domain.Models.Customers
{
    public record CustomersCreateModel : CustomersModel
    {
        public int creation_user { get; set; }
    }
}
 