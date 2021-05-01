using APBD5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD5.Services
{
    public class DbService : IDbService
    {
        private IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool IsExecuted(int rows)
        {
            if (rows >= 1)
                return true;
            else return false;
        }
        public static List<Order> orders = new List<Order>();

        public async Task<int> AddProductWarehouse(ProductWarehouse pW)
        {
            
            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                await connect.OpenAsync();
                DbTransaction tran = await connect.BeginTransactionAsync();
                try
                {
                    var idProduct = command.CommandText = $"Select * From Product Where IdProduct =" + @pW.IdProduct;
                    var idWarehouse = command.CommandText = $"Select * From Warehouse Where IdWarehouse =" + @pW.IdWarehouse;
                    var existOrder = command.CommandText = $"Select IdOrder From Order Where IdProduct =" + @pW.IdProduct + " And " + pW.Amount;
                    var orderBeenExecuted = command.CommandText = $"Select * From ProductWarehouse Where IdOrder =" + existOrder;
                    var productPrice = command.CommandText = $"Select Price From Product Where IdProduct = "+pW.IdProduct;
                    var orderPrice = pW.Amount * double.Parse(productPrice);
                    if (idProduct != null && idWarehouse != null && pW.Amount >= 0 && existOrder != null && orderBeenExecuted == null)
                    {
                        command.CommandText = $"Insert INTO Product_Warehouse" + "Valuses(@IdProduct, @IdWarehouse, @Amount, @CreatedAt";
                        command.Parameters.AddWithValue("IdProduct", pW.IdProduct);
                        command.Parameters.AddWithValue("IdWarehouse", pW.IdWarehouse);
                        command.Parameters.AddWithValue("Amount", pW.Amount);
                        command.Parameters.AddWithValue("CreatedAt", pW.CreatedAt);
                        command.Parameters.AddWithValue("Price",orderPrice);
                        command.Parameters.Clear();
                        int rowsInserted = command.ExecuteNonQuery();
                        if (!IsExecuted(rowsInserted)) throw new Exception();
                        command.CommandText = $"Update Order Set FullfilledAt = GETDATE() Where IdOredr= " + existOrder;
                    }
                    await tran.CommitAsync();
                }
                catch(SqlException exc)
                {
                    await tran.RollbackAsync();
                }catch (Exception exc)
                {
                    await tran.RollbackAsync();
                }


                connect.Close();
            }
            return 1;
            //throw new NotImplementedException();
        }
    }
    public class DbService2 : IDbService2
    {
        private IConfiguration _configuration;
        public DbService2(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool IsExecuted(int rows)
        {
            if (rows >= 1)
                return true;
            else return false;
        }
        public static List<Order> orders = new List<Order>();

        public async Task<int> AddProductWarehouseProc(ProductWarehouse pW)
        {

            using (var connect = new SqlConnection(_configuration.GetConnectionString("ProductionDb")))
            using (var command = new SqlCommand())
            {
                command.CommandType= CommandType.StoredProcedure;
                await connect.OpenAsync();
                DbTransaction tran = await connect.BeginTransactionAsync();
                try
                {
                    var idProduct = command.CommandText = $"Select * From Product Where IdProduct =" + @pW.IdProduct;
                    var idWarehouse = command.CommandText = $"Select * From Warehouse Where IdWarehouse =" + @pW.IdWarehouse;
                    var existOrder = command.CommandText = $"Select IdOrder From Order Where IdProduct =" + @pW.IdProduct + " And " + pW.Amount;
                    var orderBeenExecuted = command.CommandText = $"Select * From ProductWarehouse Where IdOrder =" + existOrder;
                    var productPrice = command.CommandText = $"Select Price From Product Where IdProduct = " + pW.IdProduct;
                    var orderPrice = pW.Amount * double.Parse(productPrice);
                    if (idProduct != null && idWarehouse != null && pW.Amount >= 0 && existOrder != null && orderBeenExecuted == null)
                    {
                        command.CommandText = $"Insert INTO Product_Warehouse" + "Valuses(@IdProduct, @IdWarehouse, @Amount, @CreatedAt";
                        command.Parameters.AddWithValue("IdProduct", pW.IdProduct);
                        command.Parameters.AddWithValue("IdWarehouse", pW.IdWarehouse);
                        command.Parameters.AddWithValue("Amount", pW.Amount);
                        command.Parameters.AddWithValue("CreatedAt", pW.CreatedAt);
                        command.Parameters.AddWithValue("Price", orderPrice);
                        command.Parameters.Clear();
                        int rowsInserted = command.ExecuteNonQuery();
                        if (!IsExecuted(rowsInserted)) throw new Exception();
                        command.CommandText = $"Update Order Set FullfilledAt = GETDATE() Where IdOredr= " + existOrder;
                    }
                    await tran.CommitAsync();
                }
                catch (SqlException exc)
                {
                    await tran.RollbackAsync();
                }
                catch (Exception exc)
                {
                    await tran.RollbackAsync();
                }


                connect.Close();
            }
            return 1;
            //throw new NotImplementedException();
        }
    }
}
