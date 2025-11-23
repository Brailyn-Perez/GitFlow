using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface;
using ShopApp.Domain.Models.Order.OrderBaseModel;


namespace ShopApp.Percistence.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderRepository> _logger;
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration, ILogger<OrderRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
       
        }
        public async Task<OperationResult<OrderModel>> CreateOrderAsync(OrderModel model)
        {
            OperationResult<OrderModel> result = new OperationResult<OrderModel>();

            try
            {
                _logger.LogInformation($"Creando una Orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarOrders", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@custid", model.custid);
                        command.Parameters.AddWithValue("@empid", model.empid);
                        command.Parameters.AddWithValue("@orderdate", model.orderdate);
                        command.Parameters.AddWithValue("@requireddate", model.requireddate);
                        command.Parameters.AddWithValue("@shippeddate", model.shippeddate);
                        command.Parameters.AddWithValue("@shipperid", model.shipperid);
                        command.Parameters.AddWithValue("@freight", model.freight);
                        command.Parameters.AddWithValue("@shipname", model.shipname);
                        command.Parameters.AddWithValue("@shipaddress", model.shipaddress);
                        command.Parameters.AddWithValue("@shipcity", model.shipcity);
                        command.Parameters.AddWithValue("@shipregion", model.shipregion);
                        command.Parameters.AddWithValue("@shippostalcode", model.shippostalcode);
                        command.Parameters.AddWithValue("@shipcountry", model.shipcountry);


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
                            _logger.LogInformation($"Orden creada satisfactoriamente. Resultado: {resultMessage} ");
                            var OrdersModel = new OrderModel
                            {
                                custid = model.custid,
                                empid = model.empid,
                                orderdate = model.orderdate,
                                requireddate = model.requireddate,
                                shippeddate = model.shippeddate,
                                shipperid = model.shipperid,
                                freight = model.freight,
                                shipname = model.shipname,
                                shipaddress = model.shipaddress,
                                shipcity = model.shipcity,
                                shipregion = model.shipregion,
                                shippostalcode = model.shippostalcode,
                                shipcountry = model.shipcountry
                            };
                            result = OperationResult<OrderModel>.Succes("Orden creada", OrdersModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear la Orden. Resultado{resultMessage}");
                            result = OperationResult<OrderModel>.Failure("no se ha podido crear la Orden");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando la Orden");
                result = OperationResult<OrderModel>.Failure($"Error creando la Orden {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<OrderModel>> DeleteOrderByIdAsync(int id)
        {
            OperationResult<OrderModel> result = new OperationResult<OrderModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion de Orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarOrders", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@orderid", id);



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

                            _logger.LogInformation($"Orden desactivada con exito");

                            var OrdersModel = new OrderModel
                            {
                                orderid = id,
                            };

                            result = OperationResult<OrderModel>.Succes("Orden desactivada correctamente.", OrdersModel);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar la Orden");
                            result = OperationResult<OrderModel>.Failure("Error al desactivar la Orden");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar la Orden");
                result = OperationResult<OrderModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<OrderModel>>> GetAllOrderAsync()
        {
            OperationResult<List<OrderModel>> result = new OperationResult<List<OrderModel>>();
            try
            {
                _logger.LogInformation("Cargando las Ordenes");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerOrders", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var Orders = new List<OrderModel>();

                            while (await reader.ReadAsync())
                            {
                                var Order = new OrderModel
                                {
                                    orderid = reader.GetInt32(reader.GetOrdinal("orderid")),
                                    custid = reader.GetInt32(reader.GetOrdinal("custid")),
                                    empid = reader.GetInt32(reader.GetOrdinal("empid")),
                                    orderdate = reader.GetDateTime(reader.GetOrdinal("orderdate")),
                                    requireddate = reader.GetDateTime(reader.GetOrdinal("requireddate")),
                                    shippeddate = reader.GetDateTime(reader.GetOrdinal("shippeddate")),
                                    shipperid = reader.GetInt32(reader.GetOrdinal("shipperid")),
                                    freight = reader.GetDecimal(reader.GetOrdinal("freight")),
                                    shipname = reader.GetString(reader.GetOrdinal("shipname")),
                                    shipaddress = reader.GetString(reader.GetOrdinal("shipaddress")),
                                    shipcity = reader.GetString(reader.GetOrdinal("shipcity")),
                                    shipregion = reader.GetString(reader.GetOrdinal("shipregion")),
                                    shippostalcode = reader.GetString(reader.GetOrdinal("shippostalcode")),
                                    shipcountry = reader.GetString(reader.GetOrdinal("shipcountry")),
                                };

                                Orders.Add(Order);
                            }

                            if (Orders.Any())
                            {
                                result = OperationResult<List<OrderModel>>.Succes("Ordenes cargadas sin problemas", Orders);
                            }
                            else
                            {
                                result = result = OperationResult<List<OrderModel>>.Failure("No se pudieron cargar todas las Ordenes");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar las Ordenes");
                result = OperationResult<List<OrderModel>>.Failure($"Error al cargar las Ordenes: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<OrderModel>> GetOrderByIdAsync(int id)
        {
            OperationResult<OrderModel> result = new OperationResult<OrderModel>();
            try
            {
                _logger.LogInformation("Cargando Orden por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerOrdersById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@orderid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                OrderModel OrderFound = new OrderModel();

                                while (await reader.ReadAsync())
                                {
                                    OrderFound.orderid = reader.GetInt32(reader.GetOrdinal("orderid"));
                                    OrderFound.custid = reader.GetInt32(reader.GetOrdinal("custid"));
                                    OrderFound.empid = reader.GetInt32(reader.GetOrdinal("empid"));
                                    OrderFound.orderdate = reader.GetDateTime(reader.GetOrdinal("orderdate"));
                                    OrderFound.requireddate = reader.GetDateTime(reader.GetOrdinal("requireddate"));
                                    OrderFound.shippeddate = reader.GetDateTime(reader.GetOrdinal("shippeddate"));
                                    OrderFound.shipperid = reader.GetInt32(reader.GetOrdinal("shipperid"));
                                    OrderFound.freight = reader.GetDecimal(reader.GetOrdinal("freight"));
                                    OrderFound.shipname = reader.GetString(reader.GetOrdinal("shipname"));
                                    OrderFound.shipaddress = reader.GetString(reader.GetOrdinal("shipaddress"));
                                    OrderFound.shipcity = reader.GetString(reader.GetOrdinal("shipcity"));
                                    OrderFound.shipregion = reader.GetString(reader.GetOrdinal("shipregion"));
                                    OrderFound.shippostalcode = reader.GetString(reader.GetOrdinal("shippostalcode"));
                                    OrderFound.shipcountry = reader.GetString(reader.GetOrdinal("shipcountry"));
                                }

                                result = OperationResult<OrderModel>.Succes("Orden por ID cargada correctamente", OrderFound);
                            }
                            else
                            {
                                result = OperationResult<OrderModel>.Failure("No se encontraron datos al cargar la Orden");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar la Orden por ID");
                result = OperationResult<OrderModel>.Failure("Error al cargar la Orden por ID");
            }
            return result;
        }

        public async Task<OperationResult<OrderModel>> UpdateOrder(OrderModel model)
        {
            OperationResult<OrderModel> result = new OperationResult<OrderModel>();

            try
            {
                _logger.LogInformation("Actualizando la Orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarOrders", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@orderid", model.orderid);
                        command.Parameters.AddWithValue("@custid", model.custid);
                        command.Parameters.AddWithValue("@empid", model.empid);
                        command.Parameters.AddWithValue("@orderdate", model.orderdate);
                        command.Parameters.AddWithValue("@requireddate", model.requireddate);
                        command.Parameters.AddWithValue("@shippeddate", model.shippeddate);
                        command.Parameters.AddWithValue("@shipperid", model.shipperid);
                        command.Parameters.AddWithValue("@freight", model.freight);
                        command.Parameters.AddWithValue("@shipname", model.shipname);
                        command.Parameters.AddWithValue("@shipaddress", model.shipaddress);
                        command.Parameters.AddWithValue("@shipcity", model.shipcity);
                        command.Parameters.AddWithValue("@shipregion", model.shipregion);
                        command.Parameters.AddWithValue("@shippostalcode", model.shippostalcode);
                        command.Parameters.AddWithValue("@shipcountry", model.shipcountry);

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

                            _logger.LogInformation($"Orden actualizada con exito");
                            var OrdersModel = new OrderModel
                            {
                                custid = model.custid,
                                empid = model.empid,
                                orderdate = model.orderdate,
                                requireddate = model.requireddate,
                                shippeddate = model.shippeddate,
                                shipperid = model.shipperid,
                                freight = model.freight,
                                shipname = model.shipname,
                                shipaddress = model.shipaddress,
                                shipcity = model.shipcity,
                                shipregion = model.shipregion,
                                shippostalcode = model.shippostalcode,
                                shipcountry = model.shipcountry
                            };
                            result = OperationResult<OrderModel>.Succes("Orden actualizada correctamente", OrdersModel);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar la Orden");
                            result = OperationResult<OrderModel>.Failure("Error al cargar la Orden");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("no se pudo actualizar la Orden");
                result = OperationResult<OrderModel>.Failure($"Error al actualizar {ex.Message}");
            }
            return result;
        }
    }
}
