

using ShopApp.Domain.Models.Products.ProductsBaseModel;

namespace ShopApp.Domain.Models.Products
{
    public record ProductsDeleteModel : ProductsModel
    {
        public int delete_user { get; set; }
    }
}
 