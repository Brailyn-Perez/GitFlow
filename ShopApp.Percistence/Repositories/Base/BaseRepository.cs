using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Base;

namespace ShopApp.Percistence.Repositories.Base
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, new ()
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BaseRepository<TModel>> _logger;
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration, ILogger<BaseRepository<TModel>> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        } 
        public Task<OperationResult<TModel>> CreateAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> DeleteByIdAsync(int id, int delete_user)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<List<TModel>>> GetAllAsync(string SPname)
        {
            OperationResult<List<TModel>> result = new OperationResult<List<TModel>>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(SPname, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync()) 
                        {
                            var list = new List<TModel>();

                            while (await reader.ReadAsync()) 
                            {
                                var item = MapReaderToModel<TModel>(reader);
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {

            }
            return result;
        }

        public Task<OperationResult<TModel>> GetnByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> UpdateAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public TModel MapReaderToModel<TModel>(SqlDataReader reader) where TModel : class, new()
        {
            var obj = new TModel(); // instancia vacía de tu modelo
            var props = typeof(TModel).GetProperties(); // obtiene las propiedades públicas del modelo

            foreach (var prop in props)
            {
                // Validar que el reader tenga la columna y que no sea NULL
                if (!reader.HasRows|| reader[prop.Name] == DBNull.Value)
                    continue;

                // Asignar el valor convertido al tipo de la propiedad
                prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], prop.PropertyType));
            }

            return obj;
        }
    }
}
