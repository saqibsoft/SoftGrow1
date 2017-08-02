using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class BankDeposits
    {
        public string connectionstring;
        public void InsertRecord(Objects.BankDeposits obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankDepositsInsert";

                cmd.Parameters.AddWithValue("@DepositID", obj.DepositID);
                cmd.Parameters.AddWithValue("@DepositDate", obj.DepositDate);
                cmd.Parameters.AddWithValue("@IsCheque", obj.IsCheque);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@ChequeDate", obj.ChequeDate);
                cmd.Parameters.AddWithValue("@SlipNo", obj.SlipNo);
                cmd.Parameters.AddWithValue("@BankAccountNo", obj.BankAccountNo);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@DepositedBy", obj.DepositedBy);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.BankDeposits obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankDepositsUpdate";

                cmd.Parameters.AddWithValue("@DepositID", obj.DepositID);
                cmd.Parameters.AddWithValue("@DepositDate", obj.DepositDate);
                cmd.Parameters.AddWithValue("@IsCheque", obj.IsCheque);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@ChequeDate", obj.ChequeDate);
                cmd.Parameters.AddWithValue("@SlipNo", obj.SlipNo);
                cmd.Parameters.AddWithValue("@BankAccountNo", obj.BankAccountNo);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@Amount", obj.Amount);
                cmd.Parameters.AddWithValue("@DepositedBy", obj.DepositedBy);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);

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
                cmd.CommandText = "SP_BankDepositsDelete";

                cmd.Parameters.AddWithValue("@DepositID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     BankDeposits.DepositID, BankDeposits.DepositDate, BankDeposits.BankAccountNo, AccountChart.AccountTitle AS BankAccountName, BankDeposits.AccountNo, 
                                                  AccountChart_1.AccountTitle AS AccountName, BankDeposits.Amount,BankDeposits.IsCheque, 
                                                  BankDeposits.ChequeNo, BankDeposits.ChequeDate, BankDeposits.SlipNo, BankDeposits.DepositedBy, BankDeposits.Narration, BankDeposits.UserID, 
                                                  BankDeposits.EntryDate, BankDeposits.IsPosted
                            FROM         BankDeposits INNER JOIN
                                                  AccountChart ON BankDeposits.BankAccountNo = AccountChart.AccountNo INNER JOIN
                                                  AccountChart AS AccountChart_1 ON BankDeposits.AccountNo = AccountChart_1.AccountNo 
                                               
                            WHERE       1=1 
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

            string vSql = "Select COALESCE(MAX(Isnull(DepositID,0)),0) + 1  as DepositID From BankDeposits ";


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
            string query = @"SELECT     BankDeposits.DepositID AS VoucherNo, CASE WHEN isnull(IsCheque, 0) = 0 THEN 'BDV' ELSE 'CDV' END AS VoucherType, 
                      BankDeposits.DepositDate AS VoucherDate, BankDeposits.Narration, BankDeposits.BankAccountNo AS AccountNo, AccountChart.AccountTitle  as AccountName, 
                      'Slip: ' + BankDeposits.SlipNo + ',' + 'Deposited by: ' + BankDeposits.DepositedBy AS Remarks, BankDeposits.Amount AS Debit, 0 AS Credit, 
                       'DEBIT' as TranStatus
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.BankAccountNo = AccountChart.AccountNo 
                        Where BankDeposits.DepositID = {0}                      
                        UNION ALL

                        SELECT     BankDeposits.DepositID AS VoucherNo, CASE WHEN isnull(IsCheque, 0) = 0 THEN 'BDV' ELSE 'CDV' END AS VoucherType, 
                                              BankDeposits.DepositDate AS VoucherDate, BankDeposits.Narration, BankDeposits.AccountNo, AccountChart.AccountTitle  as AccountName, 
                        '' AS Remarks, 0 AS Debit, 
                                              BankDeposits.Amount AS Credit,
                                              'CREDIT' AS TranStatus
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.AccountNo = AccountChart.AccountNo 
                        Where BankDeposits.DepositID = {0}  ";
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
            string vSql = "Update BankDeposits set IsPosted={1} Where DepositID={0} ";

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
