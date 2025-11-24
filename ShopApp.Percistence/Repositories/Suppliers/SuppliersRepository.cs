using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Suppliers;

using ShopApp.Domain.Models.Suppliers;

namespace ShopApp.Percistence.Repositories.Suppliers
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SuppliersRepository> _logger;
        private readonly string _connectionString;


        public SuppliersRepository(IConfiguration configuration, ILogger<SuppliersRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }
        public async Task<OperationResult<SuppliersCreateModel>> CreateSupplierAsync(SuppliersCreateModel model)
        {
            OperationResult<SuppliersCreateModel> result = new OperationResult<SuppliersCreateModel>();

            try 
            {
                _logger.LogInformation($"Creando un Suplidor");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarSuppliers", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@companyname", model.companyname);
                        command.Parameters.AddWithValue("@contactname", model.contactname);
                        command.Parameters.AddWithValue("@contacttitle", model.contacttitle);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.poscalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@fax", model.fax);
                        command.Parameters.AddWithValue("@creation_user", model.creartion_user);
                        


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
                            _logger.LogInformation($"Suplidor creado satisfactoriamente. Resultado: {resultMessage} ");
                            var suppliersCreateModel = new SuppliersCreateModel
                            {
                               companyname = model.companyname,
                               contactname = model.contactname,
                               contacttitle = model.contacttitle,
                               address = model.address,
                               city = model.city,
                               region = model.region,
                               poscalcode = model.poscalcode,
                               country = model.country,
                               phone = model.phone,
                               fax = model.fax,
                               creartion_user = model.creartion_user
                            };
                            result = OperationResult<SuppliersCreateModel>.Succes("Suplidor creado", suppliersCreateModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear el Suplidor. Resultado{resultMessage}");
                            result = OperationResult<SuppliersCreateModel>.Failure("no se ha podido crear el Suplidor");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando el Suplidor");
                result = OperationResult<SuppliersCreateModel>.Failure($"Error creando el Suplidor {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<SuppliersDeleteModel>> DeleteSupplierByIdAsync(int id, int delete_user)
        {
            OperationResult<SuppliersDeleteModel> result = new OperationResult<SuppliersDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion de Suplidor");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminerSuppliers", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@supplierid", id);
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

                            _logger.LogInformation($"Suplidor desactivado con exito");

                            var suppliersDelete = new SuppliersDeleteModel
                            {
                                supplierid = id,
                                delete_user = delete_user
                            };

                            result = OperationResult<SuppliersDeleteModel>.Succes("Suplidor desactivado correctamente.", suppliersDelete);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar el Suplidor");
                            result = OperationResult<SuppliersDeleteModel>.Failure("Error al desactivar el Suplidor");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar el Suplidor");
                result = OperationResult<SuppliersDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<SuppliersGetModel>>> GetAllSupplierAsync()
        {
            OperationResult<List<SuppliersGetModel>> result = new OperationResult<List<SuppliersGetModel>>();
            try
            {
                _logger.LogInformation("Cargando los Suplidores");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerSuppliers", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var supliers = new List<SuppliersGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var supplier = new SuppliersGetModel
                                {
                                    supplierid = reader.GetInt32(reader.GetOrdinal("supplierid")),
                                    companyname = reader.GetString(reader.GetOrdinal("companyname")),
                                    contactname = reader.GetString(reader.GetOrdinal("contactname")),
                                    contacttitle = reader.GetString(reader.GetOrdinal("contacttitle")),
                                    address = reader.GetString(reader.GetOrdinal("address")),
                                    city = reader.GetString(reader.GetOrdinal("city")),
                                    region = reader.GetString(reader.GetOrdinal("region")),
                                    poscalcode = reader.GetString(reader.GetOrdinal("postalcode")),
                                    country = reader.GetString(reader.GetOrdinal("country")),
                                    phone = reader.GetString(reader.GetOrdinal("phone")),
                                    fax = reader.GetString(reader.GetOrdinal("fax")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"))
                                };

                                supliers.Add(supplier);
                            }

                            if (supliers.Any())
                            {
                                result = OperationResult<List<SuppliersGetModel>>.Succes("Suplidores cargados sin problemas", supliers);
                            }
                            else
                            {
                                result = result = OperationResult<List<SuppliersGetModel>>.Failure("No se pudieron cargar todas los Suplidores");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los Suplidores");
                result = OperationResult<List<SuppliersGetModel>>.Failure($"Error al cargar los Suplidores: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<SuppliersGetModel>> GetSupplierByIdAsync(int id)
        {
            OperationResult<SuppliersGetModel> result = new OperationResult<SuppliersGetModel>();
            try
            {
                _logger.LogInformation("Cargando Suplidor por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerSuppliersById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@supplierid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                SuppliersGetModel SupplierFound = new SuppliersGetModel();

                                while (await reader.ReadAsync())
                                {
                                    SupplierFound.supplierid = reader.GetInt32(reader.GetOrdinal("supplierid"));
                                    SupplierFound.companyname = reader.GetString(reader.GetOrdinal("companyname"));
                                    SupplierFound.contactname = reader.GetString(reader.GetOrdinal("contactname"));
                                    SupplierFound.contacttitle = reader.GetString(reader.GetOrdinal("contacttitle"));
                                    SupplierFound.address = reader.GetString(reader.GetOrdinal("address"));
                                    SupplierFound.city = reader.GetString(reader.GetOrdinal("city"));
                                    SupplierFound.region = reader.GetString(reader.GetOrdinal("region"));
                                    SupplierFound.poscalcode = reader.GetString(reader.GetOrdinal("postalcode"));
                                    SupplierFound.country = reader.GetString(reader.GetOrdinal("country"));
                                    SupplierFound.phone = reader.GetString(reader.GetOrdinal("phone"));
                                    SupplierFound.fax = reader.GetString(reader.GetOrdinal("fax"));
                                    SupplierFound.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    SupplierFound.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));

                                }

                                result = OperationResult<SuppliersGetModel>.Succes("Suplidor por ID cargado correctamente", SupplierFound);
                            }
                            else
                            {
                                result = OperationResult<SuppliersGetModel>.Failure("No se encontraron datos al cargar el Suplidor");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar el Suplidor por ID");
                result = OperationResult<SuppliersGetModel>.Failure("Error al cargar el Suplidor por ID");
            }
            return result;
        }

        public async Task<OperationResult<SuppliersUpdateModel>> UpdateSupplier(SuppliersUpdateModel model)
        {
            OperationResult<SuppliersUpdateModel> result = new OperationResult<SuppliersUpdateModel>();

            try
            {
                _logger.LogInformation("Actualizando el Suplidor");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarSuppliers", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@supplierid", model.supplierid);
                        command.Parameters.AddWithValue("@companyname", model.companyname);
                        command.Parameters.AddWithValue("@contactname", model.contactname);
                        command.Parameters.AddWithValue("@contacttitle", model.contacttitle);
                        command.Parameters.AddWithValue("@address", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postalcode", model.poscalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@fax", model.fax);
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

                            _logger.LogInformation($"Suplidor actualizado con exito");
                            var SupplierUpdateModel = new SuppliersUpdateModel
                            {
                                supplierid = model.supplierid,
                                companyname = model.companyname,
                                contactname = model.contactname,
                                contacttitle = model.contacttitle,
                                address = model.address,
                                city = model.city,
                                region = model.region,
                                poscalcode = model.poscalcode,
                                country = model.country,
                                phone = model.phone,
                                fax = model.fax,
                                modify_user = model.modify_user
                                
                            };
                            result = OperationResult<SuppliersUpdateModel>.Succes("Suplidor actualizado correctamente", SupplierUpdateModel);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar el Suplidor");
                            result = OperationResult<SuppliersUpdateModel>.Failure("Error al cargar el Suplidor");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("no se pudo actualizar el Suplidor");
                result = OperationResult<SuppliersUpdateModel>.Failure($"Error al actualizar {ex.Message}");
            }
            return result;
        }
    }
}
