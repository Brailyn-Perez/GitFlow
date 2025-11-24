using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.OrderDetails;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;

namespace ShopApp.Percistence.Repositories.OrderDetails
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderDetailsRepository> _logger;

        public OrderDetailsRepository(IConfiguration configuration, ILogger<OrderDetailsRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection"); 
        }
        public async Task<OperationResult<OrderDetailsModel>> CreateOrderDetailsAsync(OrderDetailsModel model)
        {
            OperationResult<OrderDetailsModel> result = new OperationResult<OrderDetailsModel>();
             
            try
            {
                _logger.LogInformation($"Creando detalles de una orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarOrderDetail", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@orderid", model.orderid);
                        command.Parameters.AddWithValue("@productid", model.productid);
                        command.Parameters.AddWithValue("@unitorice", model.unitprice);
                        command.Parameters.AddWithValue("@qty", model.qty);
                        command.Parameters.AddWithValue("@discount", model.discount);
                        

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
                            _logger.LogInformation($"Detalles de la orden agregados satisfactoriamente: {resultMessage} ");
                            var OrderDetailModel = new OrderDetailsModel
                            {
                                orderid = model.orderid,
                                productid = model.productid,
                                unitprice = model.unitprice,
                                qty = model.qty,
                                discount = model.discount,
                            };
                            result = OperationResult<OrderDetailsModel>.Succes("Detalles de la orden creados", OrderDetailModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se han podido crear los detalles de la orden: {resultMessage}");
                            result = OperationResult<OrderDetailsModel>.Failure("no se ha podido crear los detalles de la orden");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando los detalles de la orden");
                result = OperationResult<OrderDetailsModel>.Failure($"Error Creando los detalles de la orden {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<List<OrderDetailsModel>>> GetAllOrderDetailsAsync()
        {
            OperationResult<List<OrderDetailsModel>> result = new OperationResult<List<OrderDetailsModel>>();
            try
            {
                _logger.LogInformation("Cargando los detalles de la orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerOrderDetails", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var OrderDetails = new List<OrderDetailsModel>();

                            while (await reader.ReadAsync())
                            {
                                var details = new OrderDetailsModel
                                {
                                    orderid = reader.GetInt32(reader.GetOrdinal("orderid")),
                                    productid = reader.GetInt32(reader.GetOrdinal("productid")),
                                    unitprice = reader.GetDecimal(reader.GetOrdinal("unitprice")),
                                    qty = reader.GetInt16(reader.GetOrdinal("qty")),
                                    discount = reader.GetDecimal(reader.GetOrdinal("discount"))
                                };

                                OrderDetails.Add(details);
                            }

                            if (OrderDetails.Any())
                            {
                                result = OperationResult<List<OrderDetailsModel>>.Succes("Detalles de la orden cargados sin problemas", OrderDetails);
                            }
                            else
                            {
                                result = result = OperationResult<List<OrderDetailsModel>>.Failure("No se encontrados detalles de ninguna orden");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los detalles de las ordenes");
                result = OperationResult<List<OrderDetailsModel>>.Failure($"Error al cargar los detalles de las ordenes: {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<OrderDetailsModel>> GetOrderDetailsByIdAsync(int id)
        {
            OperationResult<OrderDetailsModel> result = new OperationResult<OrderDetailsModel>();
            try
            {
                _logger.LogInformation("Cargando detalles de la orden por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerOrderDetailsById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@orderid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                OrderDetailsModel orderDetailsFountd = new OrderDetailsModel();

                                while (await reader.ReadAsync())
                                {
                                    orderDetailsFountd.orderid = reader.GetInt32(reader.GetOrdinal("orderid"));
                                    orderDetailsFountd.productid = reader.GetInt32(reader.GetOrdinal("productid"));
                                    orderDetailsFountd.unitprice = reader.GetDecimal(reader.GetOrdinal("unitprice"));
                                    orderDetailsFountd.qty = reader.GetInt16(reader.GetOrdinal("qty"));
                                    orderDetailsFountd.discount = reader.GetDecimal(reader.GetOrdinal("discount"));
                                }

                                result = OperationResult<OrderDetailsModel>.Succes("Detalles de la orden cargados por ID correctamente", orderDetailsFountd);
                            }
                            else
                            {
                                result = OperationResult<OrderDetailsModel>.Failure("No se encontraron los detalles cargados por ID");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar los detalles de la orden por id");
                result = OperationResult<OrderDetailsModel>.Failure("Error al cargar los detalles de la orden por id");
            }
            return result;
        }
        public async Task<OperationResult<OrderDetailsModel>> UpdateOrderDetails(OrderDetailsModel model)
        {
            OperationResult<OrderDetailsModel> result = new OperationResult<OrderDetailsModel>();

            try
            {
                _logger.LogInformation("Actualizando los detalles de la orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarOrderDetail", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@orderid", model.orderid);
                        command.Parameters.AddWithValue("@productid", model.productid);
                        command.Parameters.AddWithValue("@unitorice", model.unitprice);
                        command.Parameters.AddWithValue("@qty", model.qty);
                        command.Parameters.AddWithValue("@discount", model.discount);

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

                            _logger.LogInformation($"Detalles de orden actualizada con exito");
                            var orderDetailsModel = new OrderDetailsModel 
                            {
                                orderid = model.orderid,
                                productid = model.productid,
                                unitprice = model.unitprice,
                                qty = model.qty,
                                discount = model.discount,
                            };
                            result = OperationResult<OrderDetailsModel>.Succes("Detalles de orden actualizados correctamente", model);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar los detalles de la orden");
                            result = OperationResult<OrderDetailsModel>.Failure("Error al actualizar los detalles de la orden");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "no se pudo actualizar los detalles de la orden");
                result = OperationResult<OrderDetailsModel>.Failure($"Error al actualizar {ex.Message}");
            }
            return result;
        }
        public async Task<OperationResult<OrderDetailsModel>> DeleteOrderDetailsByIdAsync(int id)
        {
            OperationResult<OrderDetailsModel> result = new OperationResult<OrderDetailsModel>();

            try
            {
                _logger.LogInformation("Procesando de los detalles de la orden");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarOrderDetails", connection))
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

                            _logger.LogInformation($"Detalles de la orden eliminados");

                            var OrderDetails = new OrderDetailsModel
                            {
                                orderid = id,
                            };

                            result = OperationResult<OrderDetailsModel>.Succes("Detalles de la orden eliminados", OrderDetails);
                        }
                        else
                        {

                            _logger.LogWarning($"Los detalles de orden no se eliminaron");
                            result = OperationResult<OrderDetailsModel>.Failure("No hay detalles de orden para eliminar");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al eliminar los detalles de la orden");
                result = OperationResult<OrderDetailsModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }
    }
}
