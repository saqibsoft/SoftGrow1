using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class BankAccounts
    {
        public string connectionstring;
        public void InsertRecord(Objects.BankAccounts obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankAccountsInsert";

                cmd.Parameters.AddWithValue("@BankAccountID", obj.BankAccountID);
                cmd.Parameters.AddWithValue("@AccountTitle", obj.AccountTitle);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
                cmd.Parameters.AddWithValue("@BranchName", obj.BranchName);
                cmd.Parameters.AddWithValue("@AccountID", obj.AccountID);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.BankAccounts obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankAccountsUpdate";

                cmd.Parameters.AddWithValue("@BankAccountID", obj.BankAccountID);
                cmd.Parameters.AddWithValue("@AccountTitle", obj.AccountTitle);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@BranchCode", obj.BranchCode);
                cmd.Parameters.AddWithValue("@BranchName", obj.BranchName);
                cmd.Parameters.AddWithValue("@AccountID", obj.AccountID);

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
                cmd.CommandText = "SP_BankAccountsDelete";

                cmd.Parameters.AddWithValue("@BankAccountID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     BankAccounts.BankAccountID, BankAccounts.AccountTitle, BankAccounts.BankName, BankAccounts.BranchCode, BankAccounts.BranchName, 
                      BankAccounts.AccountID, AccountChart.OpeningDebit, AccountChart.OpeningCredit
FROM         BankAccounts INNER JOIN
                      AccountChart ON BankAccounts.AccountID = AccountChart.AccountNo Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(BankAccountID,0)),0) + 1  as BankAccountID From BankAccounts ";


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
