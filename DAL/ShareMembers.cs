using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class ShareMembers
    {
        public string connectionstring;
        public void InsertRecord(Objects.ShareMembers obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ShareMembersInsert";
                
                cmd.Parameters.AddWithValue("@RegistrationDate", obj.RegistrationDate);
                cmd.Parameters.AddWithValue("@MemberPic", obj.MemberPic);
                cmd.Parameters.AddWithValue("@MemberName", obj.MemberName);
                cmd.Parameters.AddWithValue("@FatherName", obj.FatherName);
                cmd.Parameters.AddWithValue("@VillageName", obj.VillageName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@CNINCNo", obj.CNINCNo);
                cmd.Parameters.AddWithValue("@Occupation", obj.Occupation);
                cmd.Parameters.AddWithValue("@PropertDetail", obj.PropertDetail);
                cmd.Parameters.AddWithValue("@PostalAddress", obj.PostalAddress);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.ShareMembers obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ShareMembersUpdate";

                cmd.Parameters.AddWithValue("@MemberID", obj.MemberID);
                cmd.Parameters.AddWithValue("@RegistrationDate", obj.RegistrationDate);
                cmd.Parameters.AddWithValue("@MemberPic", obj.MemberPic);
                cmd.Parameters.AddWithValue("@MemberName", obj.MemberName);
                cmd.Parameters.AddWithValue("@FatherName", obj.FatherName);
                cmd.Parameters.AddWithValue("@VillageName", obj.VillageName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@CNINCNo", obj.CNINCNo);
                cmd.Parameters.AddWithValue("@Occupation", obj.Occupation);
                cmd.Parameters.AddWithValue("@PropertDetail", obj.PropertDetail);
                cmd.Parameters.AddWithValue("@PostalAddress", obj.PostalAddress);
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);

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
                cmd.CommandText = "SP_ShareMembersDelete";

                cmd.Parameters.AddWithValue("@MemberID", vID);
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
FROM         ShareMembers Where 1=1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(MemberID,0)),0) + 1  as MemberID From ShareMembers ";


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
