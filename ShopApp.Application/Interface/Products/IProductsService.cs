using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Products;

namespace ShopApp.Application.Interface.Products
{
    public interface IProductsService
    {
        Task<OperationResult<ProductsCreateModel>> CreateProductsAsync(ProductsCreateModel model);
        Task<OperationResult<List<ProductsGetModel>>> GetAllProductsAsync();
        Task<OperationResult<ProductsGetModel>> GetProductsByIdAsync(int id);
        Task<OperationResult<ProductsDeleteModel>> DeleteProductsByIdAsync(int id, int delete_user);
        Task<OperationResult<ProductsUpdateModel>> UpdateProducts(ProductsUpdateModel model);
    }
}
 