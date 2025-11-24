

using ShopApp.Domain.Models.Customers.CustomersBaseModel;

namespace ShopApp.Domain.Models.Customers
{
    public record CustomersGetModel : CustomersModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
 