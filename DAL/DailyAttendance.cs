using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class DailyAttendance
    {
        public string connectionstring;
        public void InsertRecord(Objects.DailyAttendance obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DailyAttendanceInsert";
                
                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@AttendaceDateTime", obj.AttendaceDateTime);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.DailyAttendance obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_DailyAttendanceUpdate";

                cmd.Parameters.AddWithValue("@AttendanceID", obj.AttendanceID);
                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@AttendaceDateTime", obj.AttendaceDateTime);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

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
                cmd.CommandText = "SP_DailyAttendanceDelete";

                cmd.Parameters.AddWithValue("@AttendanceID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     DailyAttendance.AttendanceID, DailyAttendance.EmployeeID, DailyAttendance.EntryDate, DailyAttendance.AttendaceDateTime, DailyAttendance.Remarks, 
                      DailyAttendance.UserID, Employees.EmployeeName
FROM         DailyAttendance INNER JOIN
                      Employees ON DailyAttendance.EmployeeID = Employees.EmployeeID Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(AttendanceID,0)),0) + 1  as UnitID From DailyAttendance ";


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
