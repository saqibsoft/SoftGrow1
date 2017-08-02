using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace DAL
{
   public  class CustomerReturn
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.CustomerReturn obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerReturnInsert";
                
                cmd.Parameters.AddWithValue("@ReturnDate", obj.ReturnDate);
                //cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@SecurityReturn", obj.SecurityReturn);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                return new Database(connectionstring).ExecuteForDataTable(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.CustomerReturnBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerReturnBodyInsert";
                
                cmd.Parameters.AddWithValue("@ReturnID", obj.ReturnID);
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
        public void UpdateRecord(Objects.CustomerReturn obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerReturnUpdate";

                cmd.Parameters.AddWithValue("@ReturnID", obj.ReturnID);
                cmd.Parameters.AddWithValue("@ReturnDate", obj.ReturnDate);
                //cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@SecurityReturn", obj.SecurityReturn);
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
                cmd.CommandText = "SP_CustomerReturnDelete";

                cmd.Parameters.AddWithValue("@ReturnID", vID);
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
                cmd.CommandText = "SP_CustomerReturnBodyDelete";

                cmd.Parameters.AddWithValue("@ReturnID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     CustomerReturn.ReturnID, CustomerReturn.ReturnDate, CustomerReturn.CustomerID, Parties.PartyName, CustomerReturn.SecurityReturn, CustomerReturn.Remarks, 
                      CustomerReturnBody.ProductID, Products.ProductName, CustomerReturnBody.Qty,Isnull(CustomerReturnBody.Cost,0) as Cost, Products.UnitID, UnitsInfo.UnitTitle,0 as Units
FROM         CustomerReturn INNER JOIN
                      CustomerReturnBody ON CustomerReturn.ReturnID = CustomerReturnBody.ReturnID INNER JOIN
                      Parties ON CustomerReturn.CustomerID = Parties.PartyID INNER JOIN
                      Products ON CustomerReturnBody.ProductID = Products.ProductID INNER JOIN
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
            string query = @"SELECT     CustomerReturn.ReturnID, CustomerReturn.ReturnDate, CustomerReturn.CustomerID, Parties.PartyName, CustomerReturn.SecurityReturn, CustomerReturn.Remarks
FROM         CustomerReturn INNER JOIN
                      Parties ON CustomerReturn.CustomerID = Parties.PartyID                      
WHERE   1=1 {0} Order By  CustomerReturn.ReturnDate DESC";
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

            string vSql = "Select COALESCE(MAX(Isnull(ReturnID,0)),0) + 1  as ReturnID From CustomerReturn ";


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
