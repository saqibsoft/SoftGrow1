using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace DAL
{
  public  class Production
    {
        public string connectionstring;

        ListDictionary parameters = new ListDictionary();
        public void InsertRecord(Objects.Production obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductionInsert";

                cmd.Parameters.AddWithValue("@ProductionID", obj.ProductionID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void UpdateRecord(Objects.Production obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductionUpdate";

                cmd.Parameters.AddWithValue("@ProductionID", obj.ProductionID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void InsertRecordBody(Objects.ProductionBody obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProductionBodyInsert";
                
                cmd.Parameters.AddWithValue("@ProductionID", obj.ProductionID);
                cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                cmd.Parameters.AddWithValue("@Qty", obj.Qty);
                cmd.Parameters.AddWithValue("@Cost", obj.Cost);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void InsertConsumption(Objects.Consumption obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ConsumptionInsert";
                
                cmd.Parameters.AddWithValue("@ProductionID", obj.ProductionID);
                cmd.Parameters.AddWithValue("@ProducedProductID", obj.ProducedProductID);
                cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                cmd.Parameters.AddWithValue("@Qty", obj.Qty);
                cmd.Parameters.AddWithValue("@Cost", obj.Cost);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void DeleteRecord(Int64 vProductionID)
        {            
            try
            {

                string vSql = "Delete From Production Where ProductionID={0}";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vProductionID));
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void DeleteConsumption(Int64 vProductionID)
        {
            try
            {

                string vSql = "Delete From Consumption Where ProductionID={0} ";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vProductionID));
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void DeleteProductionBody(Int64 vProductionID)
        {
            try
            {

                string vSql = "Delete From ProductionBody Where ProductionID={0}";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vProductionID));
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public Int32 getNextID()
        {
            Int32 vNextID = 1;
            string vSql = "Select COALESCE(MAX(ProductionID),0) + 1 as ProductionID From Production";

            try
            {
                DataSet ds = new Database(connectionstring).ExecuteForDataSet(vSql);
                vNextID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vNextID;
        }

        public DataTable getProductionData(string vWhere)
        {
            string query = @"SELECT     Production.ProductionID, Production.EntryDate, Production.Narration, ProductionBody.ProductID, Products.ProductName, ProductionBody.Qty, ProductionBody.Cost, 
                      Products.UnitID, UnitsInfo.UnitTitle, 0 as Units,ProductionBody.Qty * ProductionBody.Cost as TotalValue
                    FROM         Production INNER JOIN
                      ProductionBody ON Production.ProductionID = ProductionBody.ProductionID INNER JOIN
                      Products ON ProductionBody.ProductID = Products.ProductID INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                        Where 1 = 1 {0}";
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

        public DataTable getConsumptionData(Int64 vProductionID)
        {
            string query = @"SELECT     Production.ProductionID, Production.EntryDate, Production.Narration, Consumption.ProductID, Products.ProductName, Consumption.Qty, Consumption.Cost, 
                      Products.UnitID, UnitsInfo.UnitTitle, 0 as Units, Consumption.Qty* Consumption.Cost as TotalValue
                FROM         Production INNER JOIN
                      Consumption ON Production.ProductionID = Consumption.ProductionID INNER JOIN
                      Products ON Consumption.ProductID = Products.ProductID LEFT OUTER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                        Where 1 = 1  and Production.ProductionID={0}";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vProductionID));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
    }
}
