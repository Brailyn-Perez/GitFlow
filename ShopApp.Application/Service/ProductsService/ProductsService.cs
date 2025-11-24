using ShopApp.Application.Interface.Products;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Product;
using ShopApp.Domain.Models.Products;

namespace ShopApp.Application.Service.ProductsService
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<OperationResult<ProductsCreateModel>> CreateProductsAsync(ProductsCreateModel model)
        {
            return await _productsRepository.CreateProductsAsync(model);
        }

        public async Task<OperationResult<ProductsDeleteModel>> DeleteProductsByIdAsync(int id, int delete_user)
        {
            return await _productsRepository.DeleteProductsByIdAsync(id, delete_user);
        }

        public async Task<OperationResult<List<ProductsGetModel>>> GetAllProductsAsync()
        {
            return await _productsRepository.GetAllProductsAsync();
        }

        public async Task<OperationResult<ProductsGetModel>> GetProductsByIdAsync(int id)
        {
            return await _productsRepository.GetProductsByIdAsync(id);
        }

        public async Task<OperationResult<ProductsUpdateModel>> UpdateProducts(ProductsUpdateModel model)
        {
            return await _productsRepository.UpdateProducts(model);
        }
    }
}
 