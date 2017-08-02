using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Products
    {
        public string connectionstring;
        public void InsertRecord(Objects.Products obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductsInsert";

                cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@CostPrice", obj.CostPrice);
                cmd.Parameters.AddWithValue("@SalePrice", obj.SalePrice);
                cmd.Parameters.AddWithValue("@UnitID", obj.UnitID);
                cmd.Parameters.AddWithValue("@OpeningStock", obj.OpeningStock);
                cmd.Parameters.AddWithValue("@OpeningStockValue", obj.OpeningStockValue);
                cmd.Parameters.AddWithValue("@IsRawMaterial", obj.IsRawMaterial);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Products obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductsUpdate";

                cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@CostPrice", obj.CostPrice);
                cmd.Parameters.AddWithValue("@SalePrice", obj.SalePrice);
                cmd.Parameters.AddWithValue("@UnitID", obj.UnitID);
                cmd.Parameters.AddWithValue("@OpeningStock", obj.OpeningStock);
                cmd.Parameters.AddWithValue("@OpeningStockValue", obj.OpeningStockValue);
                cmd.Parameters.AddWithValue("@IsRawMaterial", obj.IsRawMaterial);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void DeleteRecord(Int64 vID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductsDelete";

                cmd.Parameters.AddWithValue("@ProductID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     Products.ProductID, Products.ProductName, Products.CostPrice, Products.SalePrice, Products.UnitID, UnitsInfo.UnitTitle, Products.OpeningStock, 
                      Products.OpeningStockValue, Products.IsRawMaterial
FROM         Products INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID Where 1=1 {0}";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vWhere));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public int getNextNo()
        {
            int vNextID = 1;

            string vSql = "Select COALESCE(MAX(Isnull(ProductID,0)),0) + 1  as ProductID From Products ";


            try
            {
                DataSet ds = new Database(connectionstring).ExecuteForDataSet(string.Format(vSql));
                vNextID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vNextID;
        }
    }
}
