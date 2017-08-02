using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace DAL
{
   public  class StockSamples
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.StockSamples obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_StockSamplesInsert";
                
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                return new Database(connectionstring).ExecuteForDataTable(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.StockSampleBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_StockSampleBodyInsert";
                
                cmd.Parameters.AddWithValue("@SampleID", obj.SampleID);
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
        public void UpdateRecord(Objects.StockSamples obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_StockSamplesUpdate";

                cmd.Parameters.AddWithValue("@SampleID", obj.SampleID);
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

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
                cmd.CommandText = "SP_StockSamplesDelete";

                cmd.Parameters.AddWithValue("@SampleID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public void DeleteRecordBody(Int64 vID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_StockSampleBodyDelete";

                cmd.Parameters.AddWithValue("@SampleID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     StockSamples.SampleID, StockSamples.IssueDate, StockSamples.CustomerID, Parties.PartyName, StockSamples.Remarks, 
                      StockSampleBody.ProductID, Products.ProductName, StockSampleBody.Qty, StockSampleBody.Cost, Products.UnitID, UnitsInfo.UnitTitle,0 as Units
FROM         StockSamples INNER JOIN
                      StockSampleBody ON StockSamples.SampleID = StockSampleBody.SampleID INNER JOIN
                      Parties ON StockSamples.CustomerID = Parties.PartyID INNER JOIN
                      Products ON StockSampleBody.ProductID = Products.ProductID INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
WHERE   1=1 {0} ";
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
        public DataTable getSearchRecord(string vWhere)
        {
            string query = @"SELECT     StockSamples.SampleID, StockSamples.IssueDate, StockSamples.CustomerID, Parties.PartyName, StockSamples.Remarks
FROM         StockSamples INNER JOIN
                      Parties ON StockSamples.CustomerID = Parties.PartyID    
WHERE   1=1 {0} Order By  StockSamples.IssueDate DESC";
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

            string vSql = "Select COALESCE(MAX(Isnull(SampleID,0)),0) + 1  as SampleID From StockSamples ";


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
