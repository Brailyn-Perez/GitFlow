using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Categoria;
using ShopApp.Domain.Models.Categoria;

namespace ShopApp.Percistence.Repositories.Categoria
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly string _connectionString;


        public CategoriaRepository(IConfiguration configuration, ILogger<CategoriaRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }

        public async Task<OperationResult<CategoriaCreateModel>> CreateCategoriaAsync(CategoriaCreateModel model)
        {

            OperationResult<CategoriaCreateModel> result = new OperationResult<CategoriaCreateModel>();

            try
            {
                _logger.LogInformation($"Creando una Categoria");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarCategoria", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@categoryname", model.categoryname);
                        command.Parameters.AddWithValue("@description", model.description);
                        command.Parameters.AddWithValue("@creation_user", model.creation_user);

                        SqlParameter v_result = new SqlParameter("@result", System.Data.SqlDbType.VarChar)
                        {
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Output

                        };

                        command.Parameters.Add(v_result);

                        await connection.OpenAsync();

                        var RowAffected = await command.ExecuteNonQueryAsync();
                        var resultMessage = v_result.Value.ToString();

                        if (RowAffected > 0)
                        {
                            _logger.LogInformation($"Categoria creada satisfactoriamente. Resultado: {resultMessage} ");
                            var CategoriaCreateModel = new CategoriaCreateModel
                            {
                                categoryname = model.categoryname,
                                description = model.description,
                                creation_user = model.creation_user
                            };
                            result = OperationResult<CategoriaCreateModel>.Succes("Categoria creada", CategoriaCreateModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear la categoria. Resultado{resultMessage}");
                            result = OperationResult<CategoriaCreateModel>.Failure("no se ha podido crear la categoria");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex) 
            {
                _logger.LogInformation($"Error creando la categoria");
                result = OperationResult<CategoriaCreateModel>.Failure($"Error creando la Categoria {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<List<CategoriaGetModel>>> GetAllCategoriaAsync()
        {
            OperationResult<List<CategoriaGetModel>> result = new OperationResult<List<CategoriaGetModel>>();
            try
            {
                _logger.LogInformation("Cargando las Categorias");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_OptenerCategoria", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var category = new List<CategoriaGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var categoria = new CategoriaGetModel
                                {
                                    categoryid = reader.GetInt32(reader.GetOrdinal("categoryid")),
                                    categoryname = reader.GetString(reader.GetOrdinal("categoryname")),
                                    description = reader.GetString(reader.GetOrdinal("description")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"))
                                };

                                category.Add(categoria);
                            }

                            if (category.Any())
                            {
                                result = OperationResult<List<CategoriaGetModel>>.Succes("Categoria cargada sin problemas", category);
                            }
                            else
                            {
                                result = result = OperationResult<List<CategoriaGetModel>>.Failure("No se pudieron cargar todas las categorias");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogInformation("Error al cargar las categorias");
                result = OperationResult<List<CategoriaGetModel>>.Failure($"Error al cargar las categorias: {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<CategoriaGetModel>> GetCategoriaByIdAsync(int id)
        {
            OperationResult<CategoriaGetModel> result = new OperationResult<CategoriaGetModel>();
            try
            {
                _logger.LogInformation("Cargando categoria por ID");

                using (var connection = new SqlConnection(_connectionString)) 
                {
                    using (var command = new SqlCommand("SP_OptenerCategoriaByID", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@categoryid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync()) 
                        {
                            if (reader.HasRows)
                            {
                                CategoriaGetModel categoriaFound = new CategoriaGetModel();

                                while (await reader.ReadAsync())
                                {
                                    categoriaFound.categoryid = reader.GetInt32(reader.GetOrdinal("categoryid"));
                                    categoriaFound.categoryname = reader.GetString(reader.GetOrdinal("categoryname"));
                                    categoriaFound.description = reader.GetString(reader.GetOrdinal("description"));
                                    categoriaFound.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    categoriaFound.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));
                                }

                                result = OperationResult<CategoriaGetModel>.Succes("Categoria por ID cargada correctamente", categoriaFound);
                            }
                            else 
                            {
                                result = OperationResult<CategoriaGetModel>.Failure("No se encontraron datos al cargar la categoria");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogInformation("Error al buscar la categoria por ID");
                result = OperationResult<CategoriaGetModel>.Failure("Error al cargar la categoria por ID");
            }
            return result;
        }
        public async Task<OperationResult<CategoriaUpdateModel>> UpdateCategoria(CategoriaUpdateModel model)
        {
            OperationResult<CategoriaUpdateModel> result = new OperationResult<CategoriaUpdateModel>();

            try
            {
                _logger.LogInformation("Actualizando la Categoria");

                using (var connection = new SqlConnection(_connectionString)) 
                {
                    using (var command = new SqlCommand("SP_ActualizarCategoria", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@categoryid", model.categoryid);
                        command.Parameters.AddWithValue("@categoryname", model.categoryname);
                        command.Parameters.AddWithValue("@description", model.description);
                        command.Parameters.AddWithValue("@modify_user", model.modify_user);

                        SqlParameter v_result = new SqlParameter("@result", System.Data.SqlDbType.VarChar)
                        {
                            Size = 1000,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        command.Parameters.Add(v_result);

                        await connection.OpenAsync();

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var resultMessage = v_result.Value.ToString();


                        if (rowsAffected > 0)
                        {

                            _logger.LogInformation($"Categoria actualizada con exito");
                            var categorys = new CategoriaUpdateModel
                            {
                                categoryid = model.categoryid,
                                categoryname = model.categoryname,
                                description = model.description,    
                                modify_user = model.modify_user
                            };
                            result = OperationResult<CategoriaUpdateModel>.Succes("Categoria actualizada correctamente", categorys);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar la categoria");
                            result = OperationResult<CategoriaUpdateModel>.Failure("Error al cargar la categoria");
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError("no se pudo actualizar la categoria");
                result = OperationResult<CategoriaUpdateModel>.Failure($"Error al actualizar {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<CategoriaDeleteModel>> DeleteCategoriaByIdAsync(int id, int delete_user)
        {
            OperationResult<CategoriaDeleteModel> result = new OperationResult<CategoriaDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion de categoria");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarCategoria", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@categoryid", id);
                        command.Parameters.AddWithValue("@delete_user", delete_user);

                        

                        SqlParameter v_result = new SqlParameter("@result", System.Data.SqlDbType.VarChar)
                        {
                            Size = 1000,
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(v_result);

                        await connection.OpenAsync();


                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var resultMessage = v_result.Value.ToString();


                        if (rowsAffected > 0)
                        {

                            _logger.LogInformation($"Categoria desactivada con exito");

                            var categoriaDelete = new CategoriaDeleteModel
                            {
                                categoryid = id,
                                delete_user = delete_user
                            };

                            result = OperationResult<CategoriaDeleteModel>.Succes("Categoria desactivada correctamente.", categoriaDelete);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar la categoria");
                            result = OperationResult<CategoriaDeleteModel>.Failure("Error al desactivar la categoria");
                        }

                    }
                }}
            catch (Exception ex) 
            {
                _logger.LogInformation("Error al desactivar la categoria");
                result = OperationResult<CategoriaDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }
    }
}
