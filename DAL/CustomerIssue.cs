using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace DAL
{
   public class CustomerIssue
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.CustomerIssue obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerIssueInsert";
                
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@SecurityDeposit", obj.SecurityDeposit);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                return new Database(connectionstring).ExecuteForDataTable(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.CustomerIssueBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerIssueBodyInsert";
                
                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
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
        public void UpdateRecord(Objects.CustomerIssue obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CustomerIssueUpdate";

                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@SecurityDeposit", obj.SecurityDeposit);
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
                cmd.CommandText = "SP_CustomerIssueDelete";

                cmd.Parameters.AddWithValue("@IssueID", vID);
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
                cmd.CommandText = "SP_CustomerIssueBodyDelete";

                cmd.Parameters.AddWithValue("@IssueID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     CustomerIssue.IssueID, CustomerIssue.IssueDate, CustomerIssue.CustomerID, Parties.PartyName, CustomerIssue.SecurityDeposit, CustomerIssue.Remarks, 
                      CustomerIssueBody.ProductID, Products.ProductName, CustomerIssueBody.Qty, CustomerIssueBody.Cost, Products.UnitID, UnitsInfo.UnitTitle,0 as Units
FROM         CustomerIssue INNER JOIN
                      CustomerIssueBody ON CustomerIssue.IssueID = CustomerIssueBody.IssueID INNER JOIN
                      Parties ON CustomerIssue.CustomerID = Parties.PartyID INNER JOIN
                      Products ON CustomerIssueBody.ProductID = Products.ProductID INNER JOIN
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
            string query = @"SELECT     CustomerIssue.IssueID, CustomerIssue.IssueDate, CustomerIssue.CustomerID, Parties.PartyName, CustomerIssue.SecurityDeposit, CustomerIssue.Remarks
FROM         CustomerIssue INNER JOIN
                      Parties ON CustomerIssue.CustomerID = Parties.PartyID
WHERE   1=1 {0} Order By  CustomerIssue.IssueDate DESC";
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

       //......................................................
        public DataTable getParties(string vWhere, bool vWithAccounts = false, bool visJV = false)
        {
            string query = @" Select PartyID , PartyName,AccountID  from Parties Where 1 = 1 {0}";

            if (vWithAccounts == true)
            {
                query += "UNION ALL select convert(bigint,AccountNo) as AccountNo,AccountTitle from AccountChart";

                if (visJV == false) query += " Where AccountNo <> '100001'";
            }

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
       //.............................................





        public int getNextNo()
        {
            int vNextID = 1;

            string vSql = "Select COALESCE(MAX(Isnull(IssueID,0)),0) + 1  as Issueid From CustomerIssue ";


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
