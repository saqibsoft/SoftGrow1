using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Parties
    {
        public string connectionstring;
        public void InsertRecord(Objects.Parties obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PartiesInsert";

                cmd.Parameters.AddWithValue("@PartyID", obj.PartyID);
                cmd.Parameters.AddWithValue("@PartyName", obj.PartyName);
                cmd.Parameters.AddWithValue("@CNICNo", obj.CNICNo);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@NTN", obj.NTN);
                cmd.Parameters.AddWithValue("@Web", obj.Web);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@AccountID", obj.AccountID);
                cmd.Parameters.AddWithValue("@IsSupplier", obj.IsSupplier);
                cmd.Parameters.AddWithValue("@IsCustomer", obj.IsCustomer);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Parties obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PartiesUpdate";

                cmd.Parameters.AddWithValue("@PartyID", obj.PartyID);
                cmd.Parameters.AddWithValue("@PartyName", obj.PartyName);
                cmd.Parameters.AddWithValue("@CNICNo", obj.CNICNo);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@NTN", obj.NTN);
                cmd.Parameters.AddWithValue("@Web", obj.Web);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@AccountID", obj.AccountID);
                cmd.Parameters.AddWithValue("@IsSupplier", obj.IsSupplier);
                cmd.Parameters.AddWithValue("@IsCustomer", obj.IsCustomer);

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
                cmd.CommandText = "SP_PartiesDelete";

                cmd.Parameters.AddWithValue("@PartyID", vID);
                new Database(connectionstring).ExecuteNonQueryOnly(cmd);

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     Parties.PartyID, Parties.PartyName, Parties.CNICNo, Parties.ContactNo, Parties.City, Parties.Email, Parties.NTN, Parties.Web, Parties.Address, Parties.AccountID, 
                      AccountChart.OpeningDebit, AccountChart.OpeningCredit,Isnull(Parties.IsSupplier,0) as IsSupplier,Isnull(Parties.IsCustomer,0) as IsCustomer
FROM         Parties INNER JOIN
                      AccountChart ON Parties.AccountID = AccountChart.AccountNo Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(PartyID,0)),0) + 1  as PartyID From Parties ";


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
