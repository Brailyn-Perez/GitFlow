using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Shippers;
using ShopApp.Domain.Models.Categoria;
using ShopApp.Domain.Models.Shippers;


namespace ShopApp.Percistence.Repositories.Shippers
{
    public class ShippersRepository : IShippersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShippersRepository> _logger;
        private readonly string _connectionString;


        public ShippersRepository(IConfiguration configuration, ILogger<ShippersRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }
        public async Task<OperationResult<ShippersCreateModel>> CreateShippersAsync(ShippersCreateModel model)
        {
            OperationResult<ShippersCreateModel> result = new OperationResult<ShippersCreateModel>();

            try
            {
                _logger.LogInformation($"Creando un transportista:");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarShippers", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", model.name);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
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
                            _logger.LogInformation($"Transportista creada satisfactoriamente. Resultado: {resultMessage} ");
                            var Shipper = new ShippersCreateModel
                            {
                                name = model.name,
                                phone = model.phone,
                                address = model.address,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                creation_user = model.creation_user
                            };
                            result = OperationResult<ShippersCreateModel>.Succes("Transportista creado", Shipper);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear el transportista: {resultMessage}");
                            result = OperationResult<ShippersCreateModel>.Failure("no se ha podido crear el transportista");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando el transportista");
                result = OperationResult<ShippersCreateModel>.Failure($"Error Creando el transportista {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<ShippersDeleteModel>> DeleteShippersByIdAsync(int id, int delete_user)
        {
            OperationResult<ShippersDeleteModel> result = new OperationResult<ShippersDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion del transportista");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarShipper", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@shipperid", id);
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

                            _logger.LogInformation($"Transportista desactivado con exito");

                            var ShipperDelete = new ShippersDeleteModel
                            {
                                shipperid = id,
                                delete_user = delete_user
                            };

                            result = OperationResult<ShippersDeleteModel>.Succes("Transportista desactivado correctamente.", ShipperDelete);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar el transportista");
                            result = OperationResult<ShippersDeleteModel>.Failure("Error al desactivar el transportista");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar el transportista");
                result = OperationResult<ShippersDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<ShippersGetModel>>> GetAllShippersAsync()
        {
            OperationResult<List<ShippersGetModel>> result = new OperationResult<List<ShippersGetModel>>();
            try
            {
                _logger.LogInformation("Cargando los Transportistas");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerShippers", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var shippers = new List<ShippersGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var shipper = new ShippersGetModel
                                {
                                    shipperid = reader.GetInt32(reader.GetOrdinal("shipperid")),
                                    name = reader.GetString(reader.GetOrdinal("name")),
                                    phone = reader.GetString(reader.GetOrdinal("phone")),
                                    address = reader.GetString(reader.GetOrdinal("address")),
                                    city = reader.GetString(reader.GetOrdinal("city")),
                                    region = reader.GetString(reader.GetOrdinal("region")),
                                    postalcode = reader.GetString(reader.GetOrdinal("postalcode")),
                                    country = reader.GetString(reader.GetOrdinal("country")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"))
                                };

                                shippers.Add(shipper);
                            }

                            if (shippers.Any())
                            {
                                result = OperationResult<List<ShippersGetModel>>.Succes("Transportista cargado sin problemas", shippers);
                            }
                            else
                            {
                                result = result = OperationResult<List<ShippersGetModel>>.Failure("No se pudieron cargar todos los Transportistas");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los Transportistas");
                result = OperationResult<List<ShippersGetModel>>.Failure($"Error al cargar los transportistas: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<ShippersGetModel>> GetShippersByIdAsync(int id)
        {
            OperationResult<ShippersGetModel> result = new OperationResult<ShippersGetModel>();
            try
            {
                _logger.LogInformation("Cargando Transpostistas por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerShippersById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@shipperid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                ShippersGetModel shippersFound = new ShippersGetModel();

                                while (await reader.ReadAsync())
                                {
                                    shippersFound.shipperid = reader.GetInt32(reader.GetOrdinal("shipperid"));
                                    shippersFound.name = reader.GetString(reader.GetOrdinal("name"));
                                    shippersFound.phone = reader.GetString(reader.GetOrdinal("phone"));
                                    shippersFound.address = reader.GetString(reader.GetOrdinal("address"));
                                    shippersFound.city = reader.GetString(reader.GetOrdinal("city"));
                                    shippersFound.region = reader.GetString(reader.GetOrdinal("region"));
                                    shippersFound.postalcode = reader.GetString(reader.GetOrdinal("postalcode"));
                                    shippersFound.country = reader.GetString(reader.GetOrdinal("country"));
                                    shippersFound.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    shippersFound.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));

                                }

                                result = OperationResult<ShippersGetModel>.Succes("Transportistas por ID cargado correctamente", shippersFound);
                            }
                            else
                            {
                                result = OperationResult<ShippersGetModel>.Failure("No se encontraron datos al cargar el Transportista");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar el transportista por ID");
                result = OperationResult<ShippersGetModel>.Failure("Error al cargar el transportista por ID");
            }
            return result;
        }

        public async Task<OperationResult<ShippersUpdateModel>> UpdateShippers(ShippersUpdateModel model)
        {
            OperationResult<ShippersUpdateModel> result = new OperationResult<ShippersUpdateModel>();

            try
            {
                _logger.LogInformation("Actualizando el Transportista");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarShippers", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@shipperid", model.shipperid);
                        command.Parameters.AddWithValue("@name", model.name);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
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

                            _logger.LogInformation($"Transportista actualizado con exito");
                            var shippers = new ShippersUpdateModel
                            {
                                shipperid = model.shipperid,
                                name = model.name,
                                phone = model.phone,
                                address = model.address,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                modify_user = model.modify_user
                            };
                            result = OperationResult<ShippersUpdateModel>.Succes("Transportista actualizado correctamnete", shippers);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar el Transportista");
                            result = OperationResult<ShippersUpdateModel>.Failure("Error al actualizar el Transportista");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("no se pudo actualizar el Transportista");
                result = OperationResult<ShippersUpdateModel>.Failure($"Error al actualizar: {ex.Message}");
            }
            return result;
        }
    }
}
