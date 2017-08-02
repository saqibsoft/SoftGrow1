using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class Purchase
    {
        public string connectionstring;
        public void InsertRecord(Objects.Purchase obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurchaseInsert";

                cmd.Parameters.AddWithValue("@PurchaseID", obj.PurchaseID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@CashPaid", obj.CashPaid);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.PurchaseBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurchaseBodyInsert";
                
                cmd.Parameters.AddWithValue("@PurchaseID", obj.PurchaseID);
                cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
                cmd.Parameters.AddWithValue("@Qty", obj.Qty);
                cmd.Parameters.AddWithValue("@Price", obj.Price);
                cmd.Parameters.AddWithValue("@Discount", obj.Discount);
                cmd.Parameters.AddWithValue("@TotalValue", obj.TotalValue);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void UpdateRecord(Objects.Purchase obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurchaseUpdate";

                cmd.Parameters.AddWithValue("@PurchaseID", obj.PurchaseID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@CashPaid", obj.CashPaid);

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
                cmd.CommandText = "SP_PurchaseDelete";

                cmd.Parameters.AddWithValue("@PurchaseID", vID);
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
                string vSql = "Delete From PurchaseBody Where PurchaseID={0} ";

                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(string vWhere)
        {
            string query = @"SELECT     Purchase.PurchaseID, Purchase.EntryDate, Purchase.VendorID, Parties.PartyName, Purchase.GrossValue, Purchase.Narration, Purchase.CashPaid, 
                      PurchaseBody.ProductID, Products.ProductName, PurchaseBody.Qty, PurchaseBody.Price, PurchaseBody.Discount, PurchaseBody.TotalValue, Products.UnitID, 
                      UnitsInfo.UnitTitle
FROM         Purchase INNER JOIN
                      PurchaseBody ON Purchase.PurchaseID = PurchaseBody.PurchaseID INNER JOIN
                      Parties ON Purchase.VendorID = Parties.PartyID INNER JOIN
                      Products ON PurchaseBody.ProductID = Products.ProductID INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                        Where 1 = 1 {0}";
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

            string vSql = "Select COALESCE(MAX(Isnull(PurchaseID,0)),0) + 1  as PurchaseID From Purchase ";


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
