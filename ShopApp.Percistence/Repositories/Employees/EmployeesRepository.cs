using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Employees;

using ShopApp.Domain.Models.Employees;

namespace ShopApp.Percistence.Repositories.Employees
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeesRepository> _logger;
        private readonly string _connectionString;


        public EmployeesRepository(IConfiguration configuration, ILogger<EmployeesRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }
        public async Task<OperationResult<EmployeesCreateModel>> CreateEmployeesAsync(EmployeesCreateModel model)
        {
            OperationResult<EmployeesCreateModel> result = new OperationResult<EmployeesCreateModel>();
             
            try
            {
                _logger.LogInformation($"Creando un Empleado");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarEmployees", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@lastname", model.lastname);
                        command.Parameters.AddWithValue("@firstname", model.firstname);
                        command.Parameters.AddWithValue("@title", model.title);
                        command.Parameters.AddWithValue("@titleofcourtesy", model.titleofcourtesy);
                        command.Parameters.AddWithValue("@birthdate", model.birthdate);
                        command.Parameters.AddWithValue("@hiredate", model.hiredate);
                        command.Parameters.AddWithValue("@addres", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postlcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
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
                            _logger.LogInformation($"Empleado creado satisfactoriamente. Resultado: {resultMessage} ");
                            var EmployeesCreateModel = new EmployeesCreateModel
                            {
                                lastname = model.lastname,
                                firstname = model.firstname,
                                title = model.title,
                                titleofcourtesy = model.titleofcourtesy,
                                birthdate = model.birthdate,
                                hiredate = model.hiredate,
                                address = model.address,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                phone = model.phone,
                                creation_user = model.creation_user

                            };
                            result = OperationResult<EmployeesCreateModel>.Succes("Empleado creado", EmployeesCreateModel);

                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear el Empleado");
                            result = OperationResult<EmployeesCreateModel>.Failure("no se ha podido crear el Empleado");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando el Empleado");
                result = OperationResult<EmployeesCreateModel>.Failure($"Error creando el Empleado {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<EmployeesDeleteModel>> DeleteEmployeesByIdAsync(int id, int delete_user)
        {
            OperationResult<EmployeesDeleteModel> result = new OperationResult<EmployeesDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion de Empleado");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ElominarEmployees", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@empid", id);
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

                            _logger.LogInformation($"Empleado desactivado con exito");

                            var employeesDelete = new EmployeesDeleteModel
                            {
                                empid = id,
                                delete_user = delete_user
                            };

                            result = OperationResult<EmployeesDeleteModel>.Succes("Empleado desactivado correctamente.", employeesDelete);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar el Empleado");
                            result = OperationResult<EmployeesDeleteModel>.Failure("Error al desactivar el Empleado");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar el Empleado");
                result = OperationResult<EmployeesDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<EmployeesGetModel>>> GetAllEmployeesAsync()
        {
            OperationResult<List<EmployeesGetModel>> result = new OperationResult<List<EmployeesGetModel>>();
            try
            {
                _logger.LogInformation("Cargando los Empleados");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerEmployees", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var Employees = new List<EmployeesGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var Employee = new EmployeesGetModel
                                {
                                    empid = reader.GetInt32(reader.GetOrdinal("empid")),
                                    lastname = reader.GetString(reader.GetOrdinal("lastname")),
                                    firstname = reader.GetString(reader.GetOrdinal("firstname")),
                                    title = reader.GetString(reader.GetOrdinal("title")),
                                    titleofcourtesy = reader.GetString(reader.GetOrdinal("titleofcourtesy")),
                                    birthdate = reader.GetDateTime(reader.GetOrdinal("birthdate")),
                                    hiredate = reader.GetDateTime(reader.GetOrdinal("hiredate")),
                                    address = reader.GetString(reader.GetOrdinal("address")),
                                    city = reader.GetString(reader.GetOrdinal("city")),
                                    region = reader.GetString(reader.GetOrdinal("region")),
                                    postalcode = reader.GetString(reader.GetOrdinal("postalcode")),
                                    country = reader.GetString(reader.GetOrdinal("country")),
                                    phone = reader.GetString(reader.GetOrdinal("phone")),
                                    mgrid = reader.IsDBNull(reader.GetOrdinal("mgrid")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("mgrid")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"))
                                };

                                Employees.Add(Employee);
                            }

                            if (Employees.Any())
                            {
                                result = OperationResult<List<EmployeesGetModel>>.Succes("Empleados cargados sin problemas", Employees);
                            }
                            else
                            {
                                result = result = OperationResult<List<EmployeesGetModel>>.Failure("No se pudieron cargar todas los Empleados");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los Empleados");
                result = OperationResult<List<EmployeesGetModel>>.Failure($"Error al cargar los Empleados: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<EmployeesGetModel>> GetEmployeesByIdAsync(int id)
        {
            OperationResult<EmployeesGetModel> result = new OperationResult<EmployeesGetModel>();
            try
            {
                _logger.LogInformation("Cargando Empleado por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerEmployeesById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@empid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                EmployeesGetModel EmployeeFound = new EmployeesGetModel();

                                while (await reader.ReadAsync())
                                {
                                    EmployeeFound.empid = reader.GetInt32(reader.GetOrdinal("empid"));
                                    EmployeeFound.lastname = reader.GetString(reader.GetOrdinal("lastname"));
                                    EmployeeFound.firstname = reader.GetString(reader.GetOrdinal("firstname"));
                                    EmployeeFound.title = reader.GetString(reader.GetOrdinal("title"));
                                    EmployeeFound.titleofcourtesy = reader.GetString(reader.GetOrdinal("titleofcourtesy"));
                                    EmployeeFound.birthdate = reader.GetDateTime(reader.GetOrdinal("birthdate"));
                                    EmployeeFound.hiredate = reader.GetDateTime(reader.GetOrdinal("hiredate"));
                                    EmployeeFound.address = reader.GetString(reader.GetOrdinal("address"));
                                    EmployeeFound.city = reader.GetString(reader.GetOrdinal("city"));
                                    EmployeeFound.region = reader.GetString(reader.GetOrdinal("region"));
                                    EmployeeFound.postalcode = reader.GetString(reader.GetOrdinal("postalcode"));
                                    EmployeeFound.country = reader.GetString(reader.GetOrdinal("country"));
                                    EmployeeFound.phone = reader.GetString(reader.GetOrdinal("phone"));
                                    int mgridIndex = reader.GetOrdinal("mgrid");
                                    EmployeeFound.mgrid = reader.IsDBNull(mgridIndex) ? (int?)null : reader.GetInt32(mgridIndex);
                                    EmployeeFound.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    EmployeeFound.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));

                                }

                                result = OperationResult<EmployeesGetModel>.Succes("Empleado por ID cargado correctamente", EmployeeFound);
                            }
                            else
                            {
                                result = OperationResult<EmployeesGetModel>.Failure("No se encontraron datos al cargar el Empleado");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar el Empleado por ID");
                result = OperationResult<EmployeesGetModel>.Failure("Error al cargar el Empleado por ID");
            }
            return result;
        }

        public async Task<OperationResult<EmployeesUpdateModel>> UpdateEmployees(EmployeesUpdateModel model)
        {
            OperationResult<EmployeesUpdateModel> result = new OperationResult<EmployeesUpdateModel>();

            try
            {
                _logger.LogInformation("Actualizando el Empleado");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarEmployees", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@empid", model.empid);
                        command.Parameters.AddWithValue("@lastname", model.lastname);
                        command.Parameters.AddWithValue("@firstname", model.firstname);
                        command.Parameters.AddWithValue("@title", model.title);
                        command.Parameters.AddWithValue("@titleofcourtesy", model.titleofcourtesy);
                        command.Parameters.AddWithValue("@birthdate", model.birthdate);
                        command.Parameters.AddWithValue("@hiredate", model.hiredate);
                        command.Parameters.AddWithValue("@addres", model.address);
                        command.Parameters.AddWithValue("@city", model.city);
                        command.Parameters.AddWithValue("@region", model.region);
                        command.Parameters.AddWithValue("@postlcode", model.postalcode);
                        command.Parameters.AddWithValue("@country", model.country);
                        command.Parameters.AddWithValue("@phone", model.phone);
                        command.Parameters.AddWithValue("@mgrid", model.mgrid);
                        command.Parameters.AddWithValue("@modify_user", model.modiffy_user);

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

                            _logger.LogInformation($"Empleado actualizado con exito");
                            var EmployeesCreateModel = new EmployeesUpdateModel
                            {
                                empid = model.empid,
                                lastname = model.lastname,
                                firstname = model.firstname,
                                title = model.title,
                                titleofcourtesy = model.titleofcourtesy,
                                birthdate = model.birthdate,
                                hiredate = model.hiredate,
                                address = model.address,
                                city = model.city,
                                region = model.region,
                                postalcode = model.postalcode,
                                country = model.country,
                                phone = model.phone,
                                mgrid = model.mgrid,
                                modiffy_user = model.modiffy_user

                            };
                            result = OperationResult<EmployeesUpdateModel>.Succes("Empleado actualizado correctamente", EmployeesCreateModel);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar el Empleado");
                            result = OperationResult<EmployeesUpdateModel>.Failure("Error al cargar el Empleado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("no se pudo actualizar el Empleado");
                result = OperationResult<EmployeesUpdateModel>.Failure($"Error al actualizar {ex.Message}");
            }
            return result;
        }
    }
}
