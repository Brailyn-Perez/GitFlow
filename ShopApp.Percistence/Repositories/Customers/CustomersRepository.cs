using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Customers;
using ShopApp.Domain.Models.Customers;

namespace ShopApp.Percistence.Repositories.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomersRepository> _logger;
        private readonly string _connectionString;

        public CustomersRepository(IConfiguration configuration, ILogger<CustomersRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }
        public async Task<OperationResult<CustomersCreateModel>> CreateCustmersAsync(CustomersCreateModel model)
        {
            OperationResult<CustomersCreateModel> result = new OperationResult<CustomersCreateModel>();

            try
            {
                _logger.LogInformation($"Creando una cliente:");

                using (var connection = new SqlConnection(_connectionString))
                { 
                    using (var command = new SqlCommand("SP_AgregarCustomer", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@companyname", model.companyname);
                        command.Parameters.AddWithValue("@contactname", model.contactname);
                        command.Parameters.AddWithValue("@contacttitle", model.contacttitle);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@email", model.email);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@fax", model.fax);
                        command.Parameters.AddWithValue("@create_user", model.creation_user);

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
                            _logger.LogInformation($"Clientes creados satisfactoriamente. Resultado: {resultMessage} ");
                            var CustomerCreateModel = new CustomersCreateModel
                            {
                                companyname = model.companyname,
                                contactname = model.contactname,
                                contacttitle = model.contacttitle,
                                address = model.address,
                                email = model.email,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                phone = model.phone,
                                fax = model.fax,
                                creation_user = model.creation_user
                            };

                            result = OperationResult<CustomersCreateModel>.Succes("Cliente creado", CustomerCreateModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear el cliente. Resultado{resultMessage}");
                            result = OperationResult<CustomersCreateModel>.Failure("no se ha podido crear el cliente");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando el cliente");
                result = OperationResult<CustomersCreateModel>.Failure($"Error Creando el cliente {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<CustomersDeleteModel>> DeleteCustmersByIdAsync(int id, int delete_user)
        {
            OperationResult<CustomersDeleteModel> result = new OperationResult<CustomersDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion de cliente");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarCustomer", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@custid", id);
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

                            _logger.LogInformation($"Cliente desactivada con exito");

                            var CustomerDeleteModel = new CustomersDeleteModel
                            {
                                custid = id,
                                delete_user = delete_user
                            };
                            

                            result = OperationResult<CustomersDeleteModel>.Succes("Cliente desactivado con exito.", CustomerDeleteModel);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar el cliente");
                            result = OperationResult<CustomersDeleteModel>.Failure("Error al desactivar el cliente");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar el cliente");
                result = OperationResult<CustomersDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<CustomersGetModel>>> GetAllCustmersAsync()
        {
            OperationResult<List<CustomersGetModel>> result = new OperationResult<List<CustomersGetModel>>();
            try
            {
                _logger.LogInformation("Cargando los clientes");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerCustomer", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var customers = new List<CustomersGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var CustomerGetModel = new CustomersGetModel
                                {
                                    custid = reader.GetInt32(reader.GetOrdinal("custid")),
                                    companyname = reader.GetString(reader.GetOrdinal("companyname")),
                                    contactname = reader.GetString(reader.GetOrdinal("contactname")),
                                    contacttitle = reader.GetString(reader.GetOrdinal("contacttitle")),
                                    address = reader.GetString(reader.GetOrdinal("address")),
                                    email = reader.GetString(reader.GetOrdinal("email")),
                                    city = reader.GetString(reader.GetOrdinal("city")),
                                    region = reader.GetString(reader.GetOrdinal("region")),
                                    postalcode = reader.GetString(reader.GetOrdinal("postalcode")),
                                    country = reader.GetString(reader.GetOrdinal("country")),
                                    phone = reader.GetString(reader.GetOrdinal("phone")),
                                    fax = reader.GetString(reader.GetOrdinal("fax")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"))
                                };

                                customers.Add(CustomerGetModel);
                            }

                            if (customers.Any())
                            {
                                result = OperationResult<List<CustomersGetModel>>.Succes("clientes cargados sin problemas", customers);
                            }
                            else
                            {
                                result = result = OperationResult<List<CustomersGetModel>>.Failure("No se pudieron cargar todos los clientes");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los clientes");
                result = OperationResult<List<CustomersGetModel>>.Failure($"Error al cargar los clientes {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<CustomersGetModel>> GetCustmersByIdAsync(int id)
        {
            OperationResult<CustomersGetModel> result = new OperationResult<CustomersGetModel>();
            try
            {
                _logger.LogInformation("Cargando Clientes por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerCustomerbyId", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@custid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                CustomersGetModel CustomerFount = new CustomersGetModel();

                                while (await reader.ReadAsync())
                                {
                                    CustomerFount.custid = reader.GetInt32(reader.GetOrdinal("custid"));
                                    CustomerFount.companyname = reader.GetString(reader.GetOrdinal("companyname"));
                                    CustomerFount.contactname = reader.GetString(reader.GetOrdinal("contactname"));
                                    CustomerFount.contacttitle = reader.GetString(reader.GetOrdinal("contacttitle"));
                                    CustomerFount.address = reader.GetString(reader.GetOrdinal("address"));
                                    CustomerFount.email = reader.GetString(reader.GetOrdinal("email"));
                                    CustomerFount.city = reader.GetString(reader.GetOrdinal("city"));
                                    CustomerFount.region = reader.GetString(reader.GetOrdinal("region"));
                                    CustomerFount.postalcode = reader.GetString(reader.GetOrdinal("postalcode"));
                                    CustomerFount.country = reader.GetString(reader.GetOrdinal("country"));
                                    CustomerFount.phone = reader.GetString(reader.GetOrdinal("phone"));
                                    CustomerFount.fax = reader.GetString(reader.GetOrdinal("fax"));
                                    CustomerFount.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    CustomerFount.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));
                                }

                                result = OperationResult<CustomersGetModel>.Succes("Cliente por ID cargada correctamente", CustomerFount);
                            }
                            else
                            {
                                result = OperationResult<CustomersGetModel>.Failure("No se encontraron datos al cargar el cliente");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar el cliente por ID");
                result = OperationResult<CustomersGetModel>.Failure("Error al cargar el cliente por ID");
            }
            return result;
        }

        public async Task<OperationResult<CustomersUpdateModel>> UpdateCustmersAsync(CustomersUpdateModel model)
        {
            OperationResult<CustomersUpdateModel> result = new OperationResult<CustomersUpdateModel>();

            try
            {
                _logger.LogInformation($"Actualizando un cliente:");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarCustomer", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@custid", model.custid);
                        command.Parameters.AddWithValue("@companyname", model.companyname);
                        command.Parameters.AddWithValue("@contactname", model.contactname);
                        command.Parameters.AddWithValue("@contacttitle", model.contacttitle);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@email", model.email);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@fax", model.fax);
                        command.Parameters.AddWithValue("@modify_user", model.modify_user);

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
                            _logger.LogInformation($"Cliente actualizado satisfactoriamente. Resultado: {resultMessage} ");
                            var CustomerUpdateeModel = new CustomersUpdateModel
                            {
                                custid = model.custid,
                                companyname = model.companyname,
                                contactname = model.contactname,
                                contacttitle = model.contacttitle,
                                address = model.address,
                                email = model.email,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                phone = model.phone,
                                fax = model.fax,
                                modify_user = model.modify_user,
                            };

                            result = OperationResult<CustomersUpdateModel>.Succes("Cliente actualizado", CustomerUpdateeModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido actualizar el cliente. Resultado{resultMessage}");
                            result = OperationResult<CustomersUpdateModel>.Failure("no se ha podido actualizar el cliente");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error actualizando el cliente");
                result = OperationResult<CustomersUpdateModel>.Failure($"Error actualizando el cliente {ex.Message}");
            }
            return result;
        }
    }
}
