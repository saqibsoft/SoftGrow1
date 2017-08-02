using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Recovery
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.Recovery obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RecoveryInsert";
                
                cmd.Parameters.AddWithValue("@RecoveryDate", obj.RecoveryDate);
                cmd.Parameters.AddWithValue("@SalesmanID", obj.SalesmanID);
                cmd.Parameters.AddWithValue("@TotalRecovery", obj.TotalRecovery);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                return new Database(connectionstring).ExecuteForDataTable(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.RecoveryBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RecoveryBodyInsert";
                
                cmd.Parameters.AddWithValue("@RecoveryID", obj.RecoveryID);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@AmountRecoverd", obj.AmountRecoverd);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Recovery obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_RecoveryUpdate";

                cmd.Parameters.AddWithValue("@RecoveryID", obj.RecoveryID);
                cmd.Parameters.AddWithValue("@RecoveryDate", obj.RecoveryDate);
                cmd.Parameters.AddWithValue("@SalesmanID", obj.SalesmanID);
                cmd.Parameters.AddWithValue("@TotalRecovery", obj.TotalRecovery);
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
                cmd.CommandText = "SP_RecoveryDelete";

                cmd.Parameters.AddWithValue("@RecoveryID", vID);
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
                cmd.CommandText = "SP_RecoveryBodyDelete";

                cmd.Parameters.AddWithValue("@RecoveryID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     Recovery.RecoveryID, Recovery.RecoveryDate, Recovery.SalesmanID, Recovery.TotalRecovery, Recovery.UserID, Recovery.Remarks, RecoveryBody.CustomerID, 
                      Parties.PartyName, RecoveryBody.AmountRecoverd,  Employees.EmployeeName
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID INNER JOIN
                      Employees ON Recovery.SalesmanID = Employees.EmployeeID
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
            string query = @"SELECT     Recovery.RecoveryID, Recovery.RecoveryDate, Recovery.SalesmanID, Recovery.TotalRecovery, Recovery.UserID, Recovery.Remarks
                      ,  Employees.EmployeeName
FROM         Recovery INNER JOIN                                            
                      Employees ON Recovery.SalesmanID = Employees.EmployeeID
WHERE   1=1 {0} Order By  Recovery.RecoveryDate DESC";
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

            string vSql = "Select COALESCE(MAX(Isnull(RecoveryID,0)),0) + 1  as recoveryid From Recovery ";


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
