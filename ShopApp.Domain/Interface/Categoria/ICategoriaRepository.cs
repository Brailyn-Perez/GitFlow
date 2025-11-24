using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Categoria;

namespace ShopApp.Domain.Interface.Categoria
{
    public interface ICategoriaRepository
    {
        Task<OperationResult<CategoriaCreateModel>> CreateCategoriaAsync(CategoriaCreateModel model);
        Task<OperationResult<List<CategoriaGetModel>>> GetAllCategoriaAsync();
        Task<OperationResult<CategoriaGetModel>> GetCategoriaByIdAsync(int id);
        Task<OperationResult<CategoriaDeleteModel>> DeleteCategoriaByIdAsync(int id, int delete_user);
        Task<OperationResult<CategoriaUpdateModel>> UpdateCategoria(CategoriaUpdateModel model);
    }
}
 