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
   public class CompanyInfo
    {
       public string connectionstring;
       ListDictionary parameters = new ListDictionary();

       public void InsertRecord(Objects.CompanyInfo obj)
       {
           try
           {               

               SqlCommand cmd = new SqlCommand();
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "SP_InsCompanyInfo";

               cmd.Parameters.AddWithValue("@_CompanyName", obj.CompanyName);
               cmd.Parameters.AddWithValue("@_Address", obj.Address);
               cmd.Parameters.AddWithValue("@_Phone", obj.Phone);
               cmd.Parameters.AddWithValue("@_Web", obj.Web);
               cmd.Parameters.AddWithValue("@_CompanyLogo", obj.Logo);

               new Database(connectionstring).ExecuteNonQueryOnly(cmd);
               
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public void DeleteRecord()
       {
           string vSql = "Delete From CompanyInfo ";

           new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql));
       }

       public DataTable getRecord()
       {
           string query = @"Select * from CompanyInfo ";
           DataSet ds = new DataSet();

           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }
    }
}
