

using ShopApp.Domain.Models.Products.ProductsBaseModel;

namespace ShopApp.Domain.Models.Products
{
    public record ProductsGetModel : ProductsModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
