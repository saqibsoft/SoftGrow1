using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class SharesIssue
    {
        public string connectionstring;
        public void InsertRecord(Objects.SharesIssue obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SharesIssueInsert";
                
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@MemberID", obj.MemberID);
                cmd.Parameters.AddWithValue("@SchemeID", obj.SchemeID);
                cmd.Parameters.AddWithValue("@NoOfShares", obj.NoOfShares);
                cmd.Parameters.AddWithValue("@PerShareValue", obj.PerShareValue);
                cmd.Parameters.AddWithValue("@ModeofPayment", obj.ModeofPayment);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.SharesIssue obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SharesIssueUpdate";

                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@MemberID", obj.MemberID);
                cmd.Parameters.AddWithValue("@SchemeID", obj.SchemeID);
                cmd.Parameters.AddWithValue("@NoOfShares", obj.NoOfShares);
                cmd.Parameters.AddWithValue("@PerShareValue", obj.PerShareValue);
                cmd.Parameters.AddWithValue("@ModeofPayment", obj.ModeofPayment);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
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
                cmd.CommandText = "SP_SharesIssueDelete";

                cmd.Parameters.AddWithValue("@IssueID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     SharesIssue.IssueID, SharesIssue.IssueDate, SharesIssue.MemberID, SharesIssue.SchemeID, SharesIssue.NoOfShares, SharesIssue.PerShareValue, 
                      SharesIssue.ModeofPayment, SharesIssue.ChequeNo, SharesIssue.Remarks, SharesIssue.UserID, ShareScheme.SchemeTitle, ShareMembers.MemberName
FROM         SharesIssue INNER JOIN
                      ShareMembers ON SharesIssue.MemberID = ShareMembers.MemberID INNER JOIN
                      ShareScheme ON SharesIssue.SchemeID = ShareScheme.SchemeID
WHERE     (1 = 1)  {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(IssueID,0)),0) + 1  as IssueID From SharesIssue ";


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
        public DataTable getPrintData(Int64 vID)
        {
            string query = @"SELECT     SharesIssue.IssueID, SharesIssue.IssueDate, SharesIssue.MemberID, ShareMembers.MemberName, SharesIssue.NoOfShares, SharesIssue.PerShareValue, 
                      SharesIssue.ModeofPayment, SharesIssue.ChequeNo, SharesIssue.Remarks, ShareMembers.PostalAddress
FROM         SharesIssue INNER JOIN
                      ShareMembers ON SharesIssue.MemberID = ShareMembers.MemberID
WHERE     (SharesIssue.IssueID = {0})";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vID));
                return ds.Tables[0];

            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
    }
}
