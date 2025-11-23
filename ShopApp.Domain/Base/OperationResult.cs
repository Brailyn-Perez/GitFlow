

namespace ShopApp.Domain.Base
{
    public class OperationResult<TModel>
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; } = string.Empty;
        public TModel Data { get; set; }
        public OperationResult()
        {
            
        }
        public OperationResult(bool isSucces, string message, dynamic? data = null)
        {
            IsSucces = isSucces;
            Message = message;
            Data = data;
        }

        public static OperationResult<TModel> Succes(string message, dynamic? data = null) 
        {
            return new OperationResult<TModel>(true, message, data);
        }
         
        public static OperationResult<TModel> Failure(string message) 
        {
            return new OperationResult<TModel> (false, message);
        }
    }
}
