using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Vouchers
    {
        public string connectionstring;
        public void InsertRecord(Objects.VoucherHeader obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_VoucherHeaderInsert";

                cmd.Parameters.AddWithValue("@VoucherID", obj.VoucherID);
                cmd.Parameters.AddWithValue("@VoucherType", obj.VoucherType);
                cmd.Parameters.AddWithValue("@VoucherDate", obj.VoucherDate);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@PrintVoucherType", obj.PrintVoucherType);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.VoucherBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_VoucherBodyInsert";
                
                cmd.Parameters.AddWithValue("@VoucherID", obj.VoucherID);
                cmd.Parameters.AddWithValue("@VoucherType", obj.VoucherType);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@Debit", obj.Debit);
                cmd.Parameters.AddWithValue("@Credit", obj.Credit);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.VoucherHeader obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_VoucherHeaderUpdate";

                cmd.Parameters.AddWithValue("@VoucherID", obj.VoucherID);
                cmd.Parameters.AddWithValue("@VoucherType", obj.VoucherType);
                cmd.Parameters.AddWithValue("@VoucherDate", obj.VoucherDate);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@IsPosted", obj.IsPosted);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@PrintVoucherType", obj.PrintVoucherType);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void DeleteRecord(Int64 vVoucherID, string vVoucherType)
        {
            string vSql = "Delete From VoucherBody Where VoucherID={0} AND VoucherType='{1}'";
            try
            {
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vVoucherID, vVoucherType));

                vSql = "Delete From VoucherHeader Where VoucherID={0} AND VoucherType='{1}'";
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vVoucherID, vVoucherType));
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void DeleteRecordBody(Int64 vVoucherID, string vVoucherType)
        {
            string vSql = "Delete From VoucherBody Where VoucherID={0} AND VoucherType='{1}'";

            try
            {
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vVoucherID, vVoucherType));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     *
            FROM         VoucherHeader Where 1=1 {0}";
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
        public Int32 getNextID(string vVoucherType)
        {
            Int32 vNextID = 1;
            string vSql = "Select COALESCE(MAX(VoucherID),0) + 1 as VoucherID From VoucherHeader Where VoucherType='{0}'";

            try
            {
                DataSet ds = new Database(connectionstring).ExecuteForDataSet(string.Format(vSql, vVoucherType));
                vNextID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vNextID;
        }
        public DataTable getVouchersData(string vWhere)
        {
            string query = @"SELECT     VoucherHeader.VoucherID AS VoucherNo, VoucherHeader.VoucherType, VoucherHeader.VoucherDate, VoucherHeader.Narration, VoucherBody.AccountNo, 
                      ISNULL(AccountChart.AccountTitle, N'') AS AccountName, VoucherBody.Remarks, VoucherBody.Debit, VoucherBody.Credit, 
                      
(Case when isnull(VoucherBody.Debit,0) > 0 then 'DEBIT' else 'CREDIT' end ) as TranStatus,isnull(VoucherHeader.PrintVoucherType,'') as PrintVoucherType
FROM         VoucherHeader INNER JOIN
                      VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType INNER JOIN
                      AccountChart ON VoucherBody.AccountNo = AccountChart.AccountNo 
                      
WHERE     (1 = 1)
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
        public void UpdateIsPosted(Int64 vVoucherID, string vVoucherType, int vStatus)
        {
            string vSql = "Update VoucherHeader set IsPosted={2} Where VoucherID={0} AND VoucherType='{1}'";

            try
            {
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql, vVoucherID, vVoucherType, vStatus));
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}
