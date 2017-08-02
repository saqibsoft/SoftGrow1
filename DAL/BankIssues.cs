using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class BankIssues
    {
        public string connectionstring;
        public void InsertRecord(Objects.BankIssues obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankIssuesInsert";

                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@ChequeDate", obj.ChequeDate);
                cmd.Parameters.AddWithValue("@BankAccountNo", obj.BankAccountNo);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@ReceivedBy", obj.ReceivedBy);
                cmd.Parameters.AddWithValue("@IsLost", obj.IsLost);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);
                cmd.Parameters.AddWithValue("@WHTAccountNo", obj.WHTAccountNo);
                cmd.Parameters.AddWithValue("@WHTAmount", obj.WHTAmount);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.BankIssueBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankIssueBodyInsert";
                
                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.BankIssues obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankIssuesUpdate";

                cmd.Parameters.AddWithValue("@IssueID", obj.IssueID);
                cmd.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@ChequeDate", obj.ChequeDate);
                cmd.Parameters.AddWithValue("@BankAccountNo", obj.BankAccountNo);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@ReceivedBy", obj.ReceivedBy);
                cmd.Parameters.AddWithValue("@IsLost", obj.IsLost);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);
                cmd.Parameters.AddWithValue("@WHTAccountNo", obj.WHTAccountNo);
                cmd.Parameters.AddWithValue("@WHTAmount", obj.WHTAmount);

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
                cmd.CommandText = "SP_BankIssuesDelete";

                cmd.Parameters.AddWithValue("@IssueID", vID);
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
                string vSql = "Delete From BankIssueBody Where IssueID={0} ";

                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     BankIssues.IssueID, BankIssues.IssueDate, BankIssues.ChequeNo, BankIssues.ChequeDate, BankIssues.BankAccountNo, 
                      BankIssues.Amount AS TotalAmount, BankIssues.ReceivedBy, BankIssues.IsLost, BankIssues.Narration, BankIssues.UserID, BankIssues.EntryDate, 
                      BankIssues.IsPosted, AccountChart.AccountTitle AS BankAccountName, BankIssueBody.AccountNo, 
                      AccountChart_1.AccountTitle AS AccountName, BankIssueBody.Amount,ISNULL(BankIssues.WHTAmount, 0) AS WHTAmount
                    FROM         BankIssues INNER JOIN
                                            AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo INNER JOIN                                            
                                            BankIssueBody ON BankIssues.IssueID = BankIssueBody.IssueID INNER JOIN
                                            AccountChart AS AccountChart_1 ON BankIssueBody.AccountNo = AccountChart_1.AccountNo
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

            string vSql = "Select COALESCE(MAX(Isnull(IssueID,0)),0) + 1  as IssueID From BankIssues ";


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
        public DataTable getPrintData(Int64 vVoucherID)
        {
            string query = @"SELECT     BankIssues.IssueID AS VoucherNo, 'BCI' AS VoucherType, BankIssues.IssueDate AS VoucherDate, BankIssues.Narration, BankIssueBody.AccountNo, 
                      AccountChart.AccountTitle AS AccountName,'Cheq.No: ' + BankIssues.ChequeNo + ',Issue Date :' + Convert(varchar,BankIssues.ChequeDate,103) + ', Rec.by: ' + BankIssues.ReceivedBy as Remarks, BankIssueBody.Amount AS Debit, 0 AS Credit, 
                      'DEBIT' as TranStatus
                        FROM         BankIssues INNER JOIN
                                              BankIssueBody ON BankIssues.IssueID = BankIssueBody.IssueID INNER JOIN
                                              AccountChart ON BankIssueBody.AccountNo = AccountChart.AccountNo 
                        Where       BankIssues.IssueID = {0}

                        UNION ALL

                        SELECT     BankIssues.IssueID AS VoucherNo, 'BCI' AS VoucherType, BankIssues.IssueDate AS VoucherDate, BankIssues.Narration, BankIssues.BankAccountNo AS AccountNo, 
                                              AccountChart.AccountTitle AS AccountName, '' AS Remarks, 0 AS Debit, BankIssues.Amount AS Credit,  
                                               'CREDIT' AS TranStatus
                        FROM         BankIssues INNER JOIN                                              
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        Where       BankIssues.IssueID = {0}
                        
                        UNION ALL
                        
                         SELECT     BankIssues.IssueID AS VoucherNo, 'BCI' AS VoucherType, BankIssues.IssueDate AS VoucherDate, BankIssues.Narration, '200000' AS AccountNo, 
                                              'With-Holding Tax' AS AccountName, '' AS Remarks, 0 AS Debit, Isnull(BankIssues.WHTAmount,0) AS Credit,  
                                               'CREDIT' AS TranStatus
                        FROM         BankIssues INNER JOIN                                              
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        Where       BankIssues.IssueID = {0} and Isnull(BankIssues.WHTAmount,0) > 0
  ";
            DataSet ds = new DataSet();

            try
            {
                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vVoucherID));
                return ds.Tables[0];
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateIsPosted(Int64 vVoucherID, int vStatus)
        {
            string vSql = "Update BankIssues set IsPosted={1} Where IssueID={0} ";

            try
            {
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vVoucherID, vStatus));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}
