using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Categoria;

namespace ShopApp.Application.Interface.Categoria
{
    public interface ICategoriaService
    {
        Task<OperationResult<CategoriaCreateModel>> CreateCategoriaAsync(CategoriaCreateModel model);
        Task<OperationResult<List<CategoriaGetModel>>> GetAllCategoriaAsync();
        Task<OperationResult<CategoriaGetModel>> GetCategoriaByIdAsync(int id);
        Task<OperationResult<CategoriaDeleteModel>> DeleteCategoriaByIdAsync(int id, int delete_user);
        Task<OperationResult<CategoriaUpdateModel>> UpdateCategoria(CategoriaUpdateModel model);
    }
}
 