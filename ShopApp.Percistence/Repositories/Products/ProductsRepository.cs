using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Product;
using ShopApp.Domain.Models.Products;

namespace ShopApp.Percistence.Repositories.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsRepository> _logger;
        private readonly string _connectionString;
        public ProductsRepository(IConfiguration configuration, ILogger<ProductsRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("StringConection");
        }
        public async Task<OperationResult<ProductsCreateModel>> CreateProductsAsync(ProductsCreateModel model)
        {
            OperationResult<ProductsCreateModel> result = new OperationResult<ProductsCreateModel>();

            try
            {
                _logger.LogInformation($"Creando un producto");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_AgregarProducts", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@productname", model.productname);
                        command.Parameters.AddWithValue("@supplierid", model.supplierid);
                        command.Parameters.AddWithValue("@categoryid", model.categoryid);
                        command.Parameters.AddWithValue("@unitprice", model.unitprice);
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
                            _logger.LogInformation($"Producto agregado satisfactoriamente: {resultMessage} ");
                            var ProductsCreateModel = new ProductsCreateModel
                            {
                                productname = model.productname,
                                supplierid = model.supplierid,
                                categoryid = model.categoryid,
                                unitprice = model.unitprice,
                                creation_user = model.creation_user,
                            };
                            result = OperationResult<ProductsCreateModel>.Succes("Producto creado correctamente", ProductsCreateModel);
                        }
                        else
                        {
                            _logger.LogInformation($"No se ha podido crear el producto: {resultMessage}");
                            result = OperationResult<ProductsCreateModel>.Failure("no se ha podido crear el producto");

                        }
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error creando el producto");
                result = OperationResult<ProductsCreateModel>.Failure($"Error Creando el producto {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<ProductsDeleteModel>> DeleteProductsByIdAsync(int id, int delete_user)
        {
            OperationResult<ProductsDeleteModel> result = new OperationResult<ProductsDeleteModel>();

            try
            {
                _logger.LogInformation("Procesando desactivacion del producto");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_EliminarProducts", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@productid", id);
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

                            _logger.LogInformation($"Producto desactivado con exito");

                            var ProductsDelete = new ProductsDeleteModel
                            {
                                categoryid = id,
                                delete_user = delete_user
                            };

                            result = OperationResult<ProductsDeleteModel>.Succes("Producto desactivado correctamente", ProductsDelete);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al desactivar el producto");
                            result = OperationResult<ProductsDeleteModel>.Failure("Error al desactivar el producto");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al desactivar el producto");
                result = OperationResult<ProductsDeleteModel>.Failure($"Erro {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<List<ProductsGetModel>>> GetAllProductsAsync()
        {
            OperationResult<List<ProductsGetModel>> result = new OperationResult<List<ProductsGetModel>>();
            try
            {
                _logger.LogInformation("Cargando los productos");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var commad = new SqlCommand("SP_ObtenerProducts", connection))
                    {
                        commad.CommandType = System.Data.CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await commad.ExecuteReaderAsync())
                        {
                            var GetProductos = new List<ProductsGetModel>();

                            while (await reader.ReadAsync())
                            {
                                var products = new ProductsGetModel
                                {
                                    productid = reader.GetInt32(reader.GetOrdinal("productid")),
                                    productname = reader.GetString(reader.GetOrdinal("productname")),
                                    supplierid = reader.GetInt32(reader.GetOrdinal("supplierid")),
                                    categoryid = reader.GetInt32(reader.GetOrdinal("categoryid")),
                                    unitprice = reader.GetDecimal(reader.GetOrdinal("unitprice")),
                                    discontinued = reader.GetBoolean(reader.GetOrdinal("discontinued")),
                                    creation_user = reader.GetInt32(reader.GetOrdinal("creation_user")),
                                    creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"))
                                };

                                GetProductos.Add(products);
                            }

                            if (GetProductos.Any())
                            {
                                result = OperationResult<List<ProductsGetModel>>.Succes("productos cargados sin problemas", GetProductos);
                            }
                            else
                            {
                                result = result = OperationResult<List<ProductsGetModel>>.Failure("No se pudieron cargar todos los productos");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al cargar los productos");
                result = OperationResult<List<ProductsGetModel>>.Failure($"Error al cargar los productos: {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<ProductsGetModel>> GetProductsByIdAsync(int id)
        {
            OperationResult<ProductsGetModel> result = new OperationResult<ProductsGetModel>();
            try
            {
                _logger.LogInformation("Cargando productos por ID");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ObtenerProductsById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@productid", id);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                ProductsGetModel ProductsFound = new ProductsGetModel();

                                while (await reader.ReadAsync())
                                {
                                    ProductsFound.productid = reader.GetInt32(reader.GetOrdinal("productid"));
                                    ProductsFound.productname = reader.GetString(reader.GetOrdinal("productname"));
                                    ProductsFound.supplierid = reader.GetInt32(reader.GetOrdinal("supplierid"));
                                    ProductsFound.categoryid = reader.GetInt32(reader.GetOrdinal("categoryid"));
                                    ProductsFound.unitprice = reader.GetDecimal(reader.GetOrdinal("unitprice"));
                                    ProductsFound.discontinued = reader.GetBoolean(reader.GetOrdinal("discontinued"));
                                    ProductsFound.creation_date = reader.GetDateTime(reader.GetOrdinal("creation_date"));
                                    ProductsFound.creation_user = reader.GetInt32(reader.GetOrdinal("creation_user"));
                                }

                                result = OperationResult<ProductsGetModel>.Succes("producto por ID cargada correctamente", ProductsFound);
                            }
                            else
                            {
                                result = OperationResult<ProductsGetModel>.Failure("No se encontraron datos al cargar el producto");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error al buscar el producto por ID");
                result = OperationResult<ProductsGetModel>.Failure($"Error al cargar el producto por ID {ex.Message}");
            }
            return result;
        }

        public async Task<OperationResult<ProductsUpdateModel>> UpdateProducts(ProductsUpdateModel model)
        {
            OperationResult<ProductsUpdateModel> result = new OperationResult<ProductsUpdateModel>();

            try
            {
                _logger.LogInformation("Actualizando los productos");

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SP_ActualizarProducts", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@productid", model.productid);
                        command.Parameters.AddWithValue("@productname", model.productname);
                        command.Parameters.AddWithValue("@supplierid", model.supplierid);
                        command.Parameters.AddWithValue("@categoryid", model.categoryid);
                        command.Parameters.AddWithValue("@unitprice", model.unitprice);
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

                            _logger.LogInformation($"producto actualizado con exito");
                            var products = new ProductsUpdateModel
                            {
                                productid = model.productid,
                                productname = model.productname,
                                supplierid = model.supplierid,
                                categoryid = model.categoryid,
                                unitprice = model.unitprice,
                                modify_user = model.modify_user
                            };

                            result = OperationResult<ProductsUpdateModel>.Succes("Producto actualizado con exito", model);
                        }
                        else
                        {

                            _logger.LogWarning($"Error al actualizar el producto");
                            result = OperationResult<ProductsUpdateModel>.Failure("Error al actualizar el producto");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "no se pudo actualizar el producto");
                result = OperationResult<ProductsUpdateModel>.Failure($"Error al actualizar el producto {ex.Message}");
            }
            return result;
        }
    }
}
