using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class EmployeeLeaves
    {
        public string connectionstring;
        public void InsertRecord(Objects.EmployeeLeaves obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EmployeeLeavesInsert";
                
                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@LeavFrom", obj.LeavFrom);
                cmd.Parameters.AddWithValue("@LeaveTo", obj.LeaveTo);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Unpaid", obj.Unpaid);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.EmployeeLeaves obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EmployeeLeavesUpdate";

                cmd.Parameters.AddWithValue("@LeaveID", obj.LeaveID);
                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@LeavFrom", obj.LeavFrom);
                cmd.Parameters.AddWithValue("@LeaveTo", obj.LeaveTo);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@Unpaid", obj.Unpaid);

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
                cmd.CommandText = "SP_EmployeeLeavesDelete";

                cmd.Parameters.AddWithValue("@LeaveID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     EmployeeLeaves.LeaveID, EmployeeLeaves.EmployeeID, EmployeeLeaves.EntryDate, EmployeeLeaves.LeavFrom, EmployeeLeaves.LeaveTo, 
                      EmployeeLeaves.Remarks, EmployeeLeaves.UserID, EmployeeLeaves.Unpaid, Employees.EmployeeName
FROM         EmployeeLeaves INNER JOIN
                      Employees ON EmployeeLeaves.EmployeeID = Employees.EmployeeID
 Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(LeaveID,0)),0) + 1  as LeaveID From EmployeeLeaves ";

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
