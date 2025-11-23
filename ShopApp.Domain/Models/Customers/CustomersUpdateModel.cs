

using ShopApp.Domain.Models.Customers.CustomersBaseModel;

namespace ShopApp.Domain.Models.Customers
{
    public record CustomersUpdateModel : CustomersModel
    {
        public int modify_user { get; set; }
    }
}
