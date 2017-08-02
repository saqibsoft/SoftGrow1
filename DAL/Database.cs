using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;


namespace DAL
{
   public class Database
    {
       private string ConnectionString = string.Empty;

       public Database(string cs)
       {
           ConnectionString = cs;
       }

       public void ExecuteNonQueryOnly(string query)
       {
           SqlConnection con = new SqlConnection();
           SqlCommand cmd = new SqlCommand(query);

           con.ConnectionString = ConnectionString;

           try
           {
               con.Open();
               cmd.CommandType = CommandType.Text;               
               cmd.Connection = con;
               cmd.ExecuteNonQuery();
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }
       public void ExecuteNonQueryOnly(string pronam, SqlParameter[] parm)
       {
           SqlConnection con = new SqlConnection();
           SqlCommand cmd = new SqlCommand(pronam);

           con.ConnectionString = ConnectionString;

           try
           {
               con.Open();
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddRange(parm);
               cmd.Connection = con;
               cmd.ExecuteNonQuery();
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }
       public void ExecuteNonQueryOnly(string pronam, System.Collections.Specialized.ListDictionary parmeters)
       {
           SqlConnection con = new SqlConnection();
           SqlCommand cmd = new SqlCommand(pronam);

           con.ConnectionString = ConnectionString;

           try
           {
               con.Open();
               cmd.CommandType = CommandType.StoredProcedure;

               IDataParameter p;

               if (parmeters != null)
               {
                   foreach (System.Collections.DictionaryEntry parm in parmeters)
                   {
                       p = parm.Key as IDataParameter;

                       if (null == p)
                       {
                           p.ParameterName = (string)parm.Key;
                           p.Value = parm.Value;
                       }
                       else
                       {
                           p.Value = parm.Value;
                       }
                       cmd.Parameters.Add(p);
                   }
               }
               
               cmd.Connection = con;
               cmd.ExecuteNonQuery();
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }

       public void ExecuteNonQueryOnly(SqlCommand cmd)
       {
           SqlConnection con = new SqlConnection();           

           con.ConnectionString = ConnectionString;

           try
           {
               con.Open();                              
               cmd.Connection = con;
               cmd.ExecuteNonQuery();
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }
       public DataTable ExecuteForDataTable(SqlCommand cmd)
       {
           SqlConnection con = new SqlConnection();

           con.ConnectionString = ConnectionString;

           DataTable dt = new DataTable();

           try
           {
               con.Open();
               cmd.Connection = con;
               SqlDataAdapter da = new SqlDataAdapter();
               da.SelectCommand = cmd;

               DataSet ds = new DataSet();

               da.Fill(ds);
               dt = ds.Tables[0];               
               return dt;
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
               
           }
       }

       public DataSet ExecuteForDataSet(string query)
       {
           SqlConnection con = new SqlConnection();
           con.ConnectionString = ConnectionString;

           SqlCommand cmd = new SqlCommand(query);
           cmd.CommandType = CommandType.Text;
           cmd.Connection = con;

           try
           {
               con.Open();
               DataSet ds = new DataSet();
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               adp.Fill(ds);

               return ds;
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }
       public DataTable ExecuteForDataTable(string query)
       {
           SqlConnection con = new SqlConnection();
           con.ConnectionString = ConnectionString;

           SqlCommand cmd = new SqlCommand(query);
           cmd.CommandType = CommandType.Text;
           cmd.Connection = con;

           try
           {
               con.Open();
               DataSet ds = new DataSet();
               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               adp.Fill(ds);

               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }
           finally
           {
               con.Close();
           }
       }
       public SqlDataReader ExecuteForDataReader(string query)       
       {
           SqlConnection con = new SqlConnection();
           con.ConnectionString = ConnectionString;

           SqlCommand cmd = new SqlCommand(query);
           cmd.Connection = con;
           cmd.CommandType = CommandType.Text;

           SqlDataReader sreader ;

           try
           {
               con.Open();
               sreader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

           }
           catch (Exception exc)
           {

               throw exc;
           }
           return sreader;
       }


    }
}
