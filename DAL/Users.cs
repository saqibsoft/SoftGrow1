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
    public class Users
    {
        public string connectionstring;
        ListDictionary parameters = new ListDictionary();

        public void InsertRecord(Objects.Users obj)
        {
            try
            {
                parameters.Clear();
                parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8), obj.UserID);
                parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50), obj.UserName);
                parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50), obj.Password );
                parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.Bit , 1), obj.IsAdmin);
                parameters.Add(new SqlParameter("@IsSuperAdmin", SqlDbType.Bit, 1), obj.IsSuperAdmin);
                parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 1), obj.IsActive);

                new Database(connectionstring).ExecuteNonQueryOnly("SP_InsUsers", parameters);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void UpdateRecord(Objects.Users obj)
        {
            try
            {
                parameters.Clear();
                parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8), obj.UserID);
                parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50), obj.UserName);
                parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50), obj.Password);
                parameters.Add(new SqlParameter("@IsAdmin", SqlDbType.Bit, 1), obj.IsAdmin);
                parameters.Add(new SqlParameter("@IsSuperAdmin", SqlDbType.Bit, 1), obj.IsSuperAdmin);
                parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 1), obj.IsActive);

                new Database(connectionstring).ExecuteNonQueryOnly("SP_UpdUsers", parameters);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void DeleteRecord(int vUserID)
        {
            try
            {
                string vSql = "Delete From Users Where UserID={0} ";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vUserID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public DataTable getRecord(string vWhere)
        {
            string query =string.Format(@"SELECT * FROM Users Where 1=1 {0}",vWhere);
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

        public Int32 getNextID()
        {
            Int32 vNextID = 1;
            string vSql = "Select COALESCE(MAX(UserID),0) + 1 as UserID From Users";

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

    }
}
