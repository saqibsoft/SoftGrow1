using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace DAL
{
    public class UserTasks
    {
        public string connectionstring;
        ListDictionary parameters = new ListDictionary();

        public void InsertRecord(Objects.UserTasks obj)
        {
            try
            {
                parameters.Clear();                
                parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4), obj.UserID);
                parameters.Add(new SqlParameter("@TaskKey", SqlDbType.NVarChar, 50), obj.TaskKey);


                new Database(connectionstring).ExecuteNonQueryOnly("SP_InsUserTasks", parameters);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }


        public void UpdateRecord(Objects.UserTasks obj)
        {
            try
            {
                parameters.Clear();
                parameters.Add(new SqlParameter("@SerialNo", SqlDbType.Int, 4), obj.SerialNo);
                parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4), obj.UserID);
                parameters.Add(new SqlParameter("@TaskKey", SqlDbType.NVarChar, 50), obj.TaskKey);

                new Database(connectionstring).ExecuteNonQueryOnly("SP_UpdUserTasks", parameters);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void DeleteRecord(int vUserID , string vTaskKey)
        {
            try
            {
                string vSql = "Delete From UserTasks Where UserID={0} and TaskKey='{1}' ";
                 new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vUserID,vTaskKey));                
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public DataTable getRecord(int vUserID)
        {
            string query = @"SELECT ProjectTasks.TaskKey, ProjectTasks.Description, CASE WHEN UserTasks.UserID IS NULL THEN 0 ELSE 1 END AS Selected
                                FROM ProjectTasks LEFT OUTER JOIN
                                (SELECT UserID, TaskKey
                                FROM UserTasks AS UserTasks_1
                                WHERE (UserID={0})) AS Usertasks ON ProjectTasks.TaskKey = Usertasks.TaskKey";

            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vUserID));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        
    }
}
