using ShopApp.Domain.Base;

namespace ShopApp.Domain.Interface.Base
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        Task<OperationResult<TModel>> CreateAsync(TModel model);
        Task<OperationResult<List<TModel>>> GetAllAsync(string SPname);
        Task<OperationResult<TModel>> GetnByIdAsync(int id);
        Task<OperationResult<TModel>> DeleteByIdAsync(int id, int delete_user);
        Task<OperationResult<TModel>> UpdateAsync(TModel model);
    }
}
 