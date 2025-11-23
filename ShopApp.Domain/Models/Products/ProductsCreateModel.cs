

using ShopApp.Domain.Models.Products.ProductsBaseModel;

namespace ShopApp.Domain.Models.Products
{
    public record ProductsCreateModel : ProductsModel 
    {
        public int creation_user { get; set; }
    }
}
 