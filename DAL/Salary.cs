using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Salary
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.Salary obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SalaryInsert";
                
                cmd.Parameters.AddWithValue("@GenerateDate", obj.GenerateDate);
                cmd.Parameters.AddWithValue("@SalaryMonth", obj.SalaryMonth);
                cmd.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                if (obj.DesignationID >0)
                cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
                if(obj.ShiftID > 0)
                cmd.Parameters.AddWithValue("@ShiftID", obj.ShiftID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@TotalSalary", obj.TotalSalary);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                return new Database(connectionstring).ExecuteForDataTable(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.SalaryBody obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SalaryBodyInsert";
                
                cmd.Parameters.AddWithValue("@SalaryID", obj.SalaryID);
                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@Presents", obj.Presents);
                cmd.Parameters.AddWithValue("@Leaves", obj.Leaves);
                cmd.Parameters.AddWithValue("@BasicSalary", obj.BasicSalary);
                cmd.Parameters.AddWithValue("@Allowances", obj.Allowances);
                cmd.Parameters.AddWithValue("@Bonus", obj.Bonus);
                cmd.Parameters.AddWithValue("@Deduction", obj.Deduction);
                cmd.Parameters.AddWithValue("@NetSalary", obj.NetSalary);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Salary obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SalaryUpdate";

                cmd.Parameters.AddWithValue("@SalaryID", obj.SalaryID);
                cmd.Parameters.AddWithValue("@GenerateDate", obj.GenerateDate);
                cmd.Parameters.AddWithValue("@SalaryMonth", obj.SalaryMonth);
                cmd.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
                cmd.Parameters.AddWithValue("@ShiftID", obj.ShiftID);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@TotalSalary", obj.TotalSalary);
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
                cmd.CommandText = "SP_SalaryDelete";

                cmd.Parameters.AddWithValue("@SalaryID", vID);
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
                cmd.CommandText = "SP_SalaryBodyDelete";

                cmd.Parameters.AddWithValue("@SalaryID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecordMain(string vWhere)
        {
            string query = @"SELECT     Salary.SalaryID, Salary.GenerateDate, Salary.SalaryMonth, Salary.DepartmentID, Salary.DesignationID, Salary.ShiftID, Salary.Remarks, Salary.TotalSalary, 
                      Salary.UserID, Departments.DepartmentTitle, Designation.DesignationTitle, Shifts.ShiftTitle
FROM         Salary INNER JOIN
                      Departments ON Salary.DepartmentID = Departments.DepartmentID LEFT OUTER JOIN
                      Shifts ON Salary.ShiftID = Shifts.ShiftID LEFT OUTER JOIN
                      Designation ON Salary.DesignationID = Designation.DesignationID Where 1=1 {0}";
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
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     Salary.SalaryID, Salary.GenerateDate, Salary.SalaryMonth, Salary.DepartmentID, Salary.DesignationID, Salary.ShiftID, Salary.Remarks, Salary.TotalSalary, 
                      Salary.UserID, Departments.DepartmentTitle, Designation.DesignationTitle, Shifts.ShiftTitle, SalaryBody.EmployeeID, Employees.EmployeeName, 
                      SalaryBody.Presents, SalaryBody.Leaves, SalaryBody.BasicSalary, SalaryBody.Allowances, SalaryBody.Bonus, SalaryBody.Deduction, SalaryBody.NetSalary
FROM         Salary INNER JOIN
                      Departments ON Salary.DepartmentID = Departments.DepartmentID INNER JOIN
                      SalaryBody ON Salary.SalaryID = SalaryBody.SalaryID INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID LEFT OUTER JOIN
                      Shifts ON Salary.ShiftID = Shifts.ShiftID LEFT OUTER JOIN
                      Designation ON Salary.DesignationID = Designation.DesignationID Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(SalaryID,0)),0) + 1  as SalaryID From Salary ";

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
        public DataTable getEmployees(string vWhere,int vMonth,int vYear)
        {
            string query = @"SELECT     Employees.EmployeeID, Employees.EmployeeName, Employees.BasicSalary, ISNULL(Attendance.Presents, 0) AS Presents, ISNULL(Leaves.LeaveDays, 0) 
                      AS Leaves
FROM         Employees LEFT OUTER JOIN
                          (SELECT     EmployeeID, ISNULL(SUM(DATEDIFF(Day, LeavFrom, LeaveTo + 1)), 0) AS LeaveDays
                            FROM          EmployeeLeaves
                            WHERE      (YEAR(LeavFrom) = {2}) AND (MONTH(LeavFrom) = {1})
                            GROUP BY EmployeeID) AS Leaves ON Employees.EmployeeID = Leaves.EmployeeID LEFT OUTER JOIN
                          (SELECT     EmployeeID, COUNT(*) AS Presents
                            FROM          DailyAttendance
                            WHERE      (MONTH(AttendaceDateTime) = {1}) AND (YEAR(AttendaceDateTime) = {2})
                            GROUP BY EmployeeID) AS Attendance ON Employees.EmployeeID = Attendance.EmployeeID
WHERE     (isnull(Employees.InActive,0) = 0) {0}
";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vWhere,vMonth,vYear));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
    }
}
