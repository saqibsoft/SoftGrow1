using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class ShareScheme
    {
        public string connectionstring;
        public void InsertRecord(Objects.ShareScheme obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ShareSchemeInsert";
                
                cmd.Parameters.AddWithValue("@SchemeTitle", obj.SchemeTitle);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@TotalShares", obj.TotalShares);
                cmd.Parameters.AddWithValue("@PerShareValue", obj.PerShareValue);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.ShareScheme obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ShareSchemeUpdate";

                cmd.Parameters.AddWithValue("@SchemeID", obj.SchemeID);
                cmd.Parameters.AddWithValue("@SchemeTitle", obj.SchemeTitle);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@TotalShares", obj.TotalShares);
                cmd.Parameters.AddWithValue("@PerShareValue", obj.PerShareValue);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void DeleteRecord(int vID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ShareSchemeDelete";

                cmd.Parameters.AddWithValue("@SchemeID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     *
FROM         ShareScheme Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(SchemeID,0)),0) + 1  as SchemeID From ShareScheme ";


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
        public Int64 getAvailableShares(int vSchemeID)
        {
            Int64 vNoOfShares = 0;

            string vSql = @"SELECT     ShareScheme.TotalShares - ISNULL(sold.SoldShares, 0) AS AvailableShares
                FROM         ShareScheme LEFT OUTER JOIN
                                          (SELECT     SchemeID, SUM(ISNULL(NoOfShares, 0)) AS SoldShares
                                            FROM          SharesIssue
                                            GROUP BY SchemeID) AS sold ON ShareScheme.SchemeID = sold.SchemeID
                WHERE     (ShareScheme.SchemeID = {0})";


            try
            {
                DataTable dt = new Database(connectionstring).ExecuteForDataTable(string.Format(vSql,vSchemeID));
                
                if(dt.Rows.Count >0)
                vNoOfShares = Convert.ToInt64(dt.Rows[0][0]);

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vNoOfShares;
        }
        
    }
}
