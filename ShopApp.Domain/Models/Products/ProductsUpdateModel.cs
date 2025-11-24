

using ShopApp.Domain.Models.Products.ProductsBaseModel;

namespace ShopApp.Domain.Models.Products
{
    public record ProductsUpdateModel : ProductsModel
    {
        public int modify_user { get; set; }
    }
}
 