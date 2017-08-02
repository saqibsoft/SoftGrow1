using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class ProfitDistribution
    {
        public string connectionstring;
        public DataTable InsertRecord(Objects.ProfitDistribution obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProfitDistributionInsert";
                
                cmd.Parameters.AddWithValue("@DistributionDate", obj.DistributionDate);
                cmd.Parameters.AddWithValue("@SchemeID", obj.SchemeID);
                cmd.Parameters.AddWithValue("@NetProfit", obj.NetProfit);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                DataTable dt = new Database(connectionstring).ExecuteForDataTable(cmd);
                return dt;
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.ProfitDistDetail obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProfitDistDetailInsert";
                
                cmd.Parameters.AddWithValue("@DistributionID", obj.DistributionID);
                cmd.Parameters.AddWithValue("@MemberID", obj.MemberID);
                cmd.Parameters.AddWithValue("@ProfitRate", obj.ProfitRate);
                cmd.Parameters.AddWithValue("@ProfitAmount", obj.ProfitAmount);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.ProfitDistribution obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProfitDistributionUpdate";

                cmd.Parameters.AddWithValue("@DistributionID", obj.DistributionID);
                cmd.Parameters.AddWithValue("@DistributionDate", obj.DistributionDate);
                cmd.Parameters.AddWithValue("@SchemeID", obj.SchemeID);
                cmd.Parameters.AddWithValue("@NetProfit", obj.NetProfit);
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
                cmd.CommandText = "SP_ProfitDistributionDelete";

                cmd.Parameters.AddWithValue("@DistributionID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public void DeleteRecordBody(int vID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ProfitDistDetailDelete";

                cmd.Parameters.AddWithValue("@DistributionID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @" SELECT     ProfitDistribution.DistributionID, ProfitDistribution.DistributionDate, ProfitDistribution.SchemeID, ShareScheme.SchemeTitle, ProfitDistribution.NetProfit, 
                      ProfitDistribution.Remarks, ProfitDistDetail.MemberID, ShareMembers.MemberName, ProfitDistDetail.ProfitRate, ProfitDistDetail.ProfitAmount, 
                      SharesBuying.BoughtShares
FROM         ProfitDistribution INNER JOIN
                      ProfitDistDetail ON ProfitDistribution.DistributionID = ProfitDistDetail.DistributionID INNER JOIN
                      ShareScheme ON ProfitDistribution.SchemeID = ShareScheme.SchemeID INNER JOIN
                      ShareMembers ON ProfitDistDetail.MemberID = ShareMembers.MemberID INNER JOIN
                          (SELECT     SchemeID, MemberID, SUM(NoOfShares) AS BoughtShares
                            FROM          SharesIssue
                            GROUP BY SchemeID, MemberID) AS SharesBuying ON ShareMembers.MemberID = SharesBuying.MemberID AND 
                      ProfitDistribution.SchemeID = SharesBuying.SchemeID
                    WHERE      1=1 
  {0}";
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
        public DataTable getRecordSearch(string vWhere)
        {
            string query = @" SELECT     ProfitDistribution.DistributionID, ProfitDistribution.DistributionDate, ProfitDistribution.SchemeID, ProfitDistribution.NetProfit, ProfitDistribution.Remarks, 
                      ProfitDistribution.UserID, ShareScheme.SchemeTitle
FROM         ProfitDistribution INNER JOIN
                      ShareScheme ON ProfitDistribution.SchemeID = ShareScheme.SchemeID 
                    WHERE      1=1 
  {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(DistributionID,0)),0) + 1  as DistributionID From ProfitDistribution ";


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
        public DataTable GetSchemeMembers(int vSchemeID)
        {
            string query = @" SELECT     SharesRec.MemberID, ShareMembers.MemberName, ShareScheme.TotalShares, SharesRec.BoughtShares
FROM         ShareScheme INNER JOIN
                          (SELECT     SchemeID, MemberID, SUM(NoOfShares) AS BoughtShares
                            FROM          SharesIssue
                            GROUP BY SchemeID, MemberID) AS SharesRec ON ShareScheme.SchemeID = SharesRec.SchemeID INNER JOIN
                      ShareMembers ON SharesRec.MemberID = ShareMembers.MemberID
Where ShareScheme.SchemeID = {0}
";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vSchemeID));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
    }
}
