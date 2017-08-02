using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Employees
    {
        public string connectionstring;
        public void InsertRecord(Objects.Employees obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EmployeesInsert";

                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@FatherName", obj.FatherName);
                cmd.Parameters.AddWithValue("@JoiningDate", obj.JoiningDate);
                cmd.Parameters.AddWithValue("@CNIC", obj.CNIC);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@IsSalesman", obj.IsSalesman);
                cmd.Parameters.AddWithValue("@BasicSalary", obj.BasicSalary);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
                cmd.Parameters.AddWithValue("@ShiftID", obj.ShiftID);
                cmd.Parameters.AddWithValue("@InActive", obj.InActive);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Employees obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_EmployeesUpdate";

                cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@FatherName", obj.FatherName);
                cmd.Parameters.AddWithValue("@JoiningDate", obj.JoiningDate);
                cmd.Parameters.AddWithValue("@CNIC", obj.CNIC);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@IsSalesman", obj.IsSalesman);
                cmd.Parameters.AddWithValue("@BasicSalary", obj.BasicSalary);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                cmd.Parameters.AddWithValue("@DesignationID", obj.DesignationID);
                cmd.Parameters.AddWithValue("@ShiftID", obj.ShiftID);
                cmd.Parameters.AddWithValue("@InActive", obj.InActive);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
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
                cmd.CommandText = "SP_EmployeesDelete";

                cmd.Parameters.AddWithValue("@EmployeeID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"
SELECT     Employees.EmployeeID, Employees.EmployeeName, Employees.FatherName, Employees.JoiningDate, Employees.CNIC, Employees.ContactNo, Employees.Address,
                       Employees.IsSalesman, Employees.BasicSalary, Employees.DepartmentID, Departments.DepartmentTitle, Employees.DesignationID, Designation.DesignationTitle, 
                      Employees.ShiftID, Shifts.ShiftTitle, Employees.InActive, Employees.AccountNo
FROM         Employees LEFT OUTER JOIN
                      Shifts ON Employees.ShiftID = Shifts.ShiftID LEFT OUTER JOIN
                      Designation ON Employees.DesignationID = Designation.DesignationID LEFT OUTER JOIN
                      Departments ON Employees.DepartmentID = Departments.DepartmentID Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(EmployeeID,0)),0) + 1  as EmployeeID From Employees ";


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
