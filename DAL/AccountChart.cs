using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
   public class AccountChart
    {
        public string connectionstring;
        public void InsertRecord(Objects.AccountChart obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_AccountChartInsert";

                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@AccountTitle", obj.AccountTitle);
                cmd.Parameters.AddWithValue("@AccountType", obj.AccountType);
                cmd.Parameters.AddWithValue("@AccountSubType", obj.AccountSubType);
                cmd.Parameters.AddWithValue("@OpeningDebit", obj.OpeningDebit);
                cmd.Parameters.AddWithValue("@OpeningCredit", obj.OpeningCredit);
                cmd.Parameters.AddWithValue("@IsParty", obj.IsParty);
                cmd.Parameters.AddWithValue("@IsBank", obj.IsBank);
                cmd.Parameters.AddWithValue("@IsEditable", obj.IsEditable);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.AccountChart obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_AccountChartUpdate";

                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@AccountTitle", obj.AccountTitle);
                cmd.Parameters.AddWithValue("@AccountType", obj.AccountType);
                cmd.Parameters.AddWithValue("@AccountSubType", obj.AccountSubType);
                cmd.Parameters.AddWithValue("@OpeningDebit", obj.OpeningDebit);
                cmd.Parameters.AddWithValue("@OpeningCredit", obj.OpeningCredit);
                cmd.Parameters.AddWithValue("@IsParty", obj.IsParty);
                cmd.Parameters.AddWithValue("@IsBank", obj.IsBank);
                cmd.Parameters.AddWithValue("@IsEditable", obj.IsEditable);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void DeleteRecord(string vID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_AccountChartDelete";

                cmd.Parameters.AddWithValue("@AccountNo", vID);
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
            FROM         AccountChart Where 1=1 {0}";
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
        public Int64 getNextNo(string vAccountType)
        {
            Int64 vNextID = 1;
            string preFix = string.Empty;
            string tmpNo = "0000";

            string vSql = "Select COALESCE(MAX(Convert(bigint,AccountNo)),0) + 1 as AccountNo From AccountChart where AccountType='{0}'";

            if (vAccountType == "ASSET")
            { preFix = "10000"; }
            else if (vAccountType == "LIABILITY")
            { preFix = "20000"; }
            else if (vAccountType == "EXPENSE")
            { preFix = "30000"; }
            else if (vAccountType == "REVENUE")
            { preFix = "40000"; }
            else if (vAccountType == "CAPITAL")
            { preFix = "50000"; }

            try
            {
                DataSet ds = new Database(connectionstring).ExecuteForDataSet(string.Format(vSql, vAccountType));
                vNextID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                if (vNextID == 1)
                {
                    //tmpNo = tmpNo.Substring(0, 3 - vNextID.ToString().Length);
                    tmpNo = preFix + vNextID.ToString();
                }
                else
                {
                    tmpNo = vNextID.ToString();
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
            return Int64.Parse(tmpNo);
        }

        public void UpdateOpenings(string vID, decimal vDebit, decimal vCredit)
        {
            try
            {
                string vSql = "Update AccountChart Set OpeningDebit={0}, OpeningCredit={1} Where AccountNo='{2}' ";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vDebit,vCredit,vID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

    }
}
