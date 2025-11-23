using Microsoft.Extensions.Logging;
using ShopApp.Application.Interface.Categoria;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Categoria;
using ShopApp.Domain.Models.Categoria;

namespace ShopApp.Application.Service.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<OperationResult<CategoriaCreateModel>> CreateCategoriaAsync(CategoriaCreateModel model)
        {
            return await _categoriaRepository.CreateCategoriaAsync(model);
        }

        public async Task<OperationResult<CategoriaDeleteModel>> DeleteCategoriaByIdAsync(int id, int delete_user)
        {
            return await _categoriaRepository.DeleteCategoriaByIdAsync(id, delete_user);
        } 

        public async Task<OperationResult<List<CategoriaGetModel>>> GetAllCategoriaAsync()
        {
            return await _categoriaRepository.GetAllCategoriaAsync();
        }

        public async Task<OperationResult<CategoriaGetModel>> GetCategoriaByIdAsync(int id)
        {
            return await _categoriaRepository.GetCategoriaByIdAsync(id);
        }

        public async Task<OperationResult<CategoriaUpdateModel>> UpdateCategoria(CategoriaUpdateModel model)
        {
            return await _categoriaRepository.UpdateCategoria(model);
        }
    }
}
