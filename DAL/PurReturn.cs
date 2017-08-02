using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class PurReturn
    {
        public string connectionstring;
        public void InsertRecord(Objects.PurReturn obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurReturnInsert";

                cmd.Parameters.AddWithValue("@PurReturnID", obj.PurReturnID);
                cmd.Parameters.AddWithValue("@PurchaseID", obj.PurchaseID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@CashReceived", obj.CashReceived);

                new Database(connectionstring).ExecuteNonQueryOnly(cmd);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }
        public void InsertRecordBody(Objects.PurReturnBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurReturnBodyInsert";
                
                cmd.Parameters.AddWithValue("@PurReturnID", obj.PurReturnID);
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
        public void UpdateRecord(Objects.PurReturn obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_PurReturnUpdate";

                cmd.Parameters.AddWithValue("@PurReturnID", obj.PurReturnID);
                cmd.Parameters.AddWithValue("@PurchaseID", obj.PurchaseID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@VendorID", obj.VendorID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@Narration", obj.Narration);
                cmd.Parameters.AddWithValue("@UserID", obj.UserID);
                cmd.Parameters.AddWithValue("@CashReceived", obj.CashReceived);

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
                cmd.CommandText = "SP_PurReturnDelete";

                cmd.Parameters.AddWithValue("@PurReturnID", vID);
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
                string vSql = "Delete From PurReturnBody Where PurReturnID={0} ";

                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(Int64 vPurRetID)
        {
            string query = @"SELECT     Isnull(PurReturn.PurReturnID,0) as PurReturnID, Purchase.PurchaseID, PurReturn.EntryDate, Purchase.EntryDate AS PurchaseDate, PurReturn.VendorID, Parties.PartyName, 
                                  PurReturn.GrossValue, PurReturn.Narration, PurReturn.CashReceived, (CASE WHEN PurReturn.ProductID IS NULL THEN 'false' ELSE 'true' END) AS Selected, 
                                  PurchaseBody.ProductID, Products.ProductName, Products.UnitID, UnitsInfo.UnitTitle, 0 AS Units, ISNULL(PurReturn.Qty, 
                                  PurchaseBody.Qty) AS Qty, ISNULL(PurReturn.Price, PurchaseBody.Price) AS Price, ISNULL(PurReturn.Discount, PurchaseBody.Discount) AS Discount, 
                                  ISNULL(PurReturn.TotalValue, PurchaseBody.TotalValue) AS TotalValue
            FROM         UnitsInfo INNER JOIN
                                  PurchaseBody INNER JOIN
                                  Products ON PurchaseBody.ProductID = Products.ProductID ON UnitsInfo.UnitID = Products.UnitID INNER JOIN
                                  Parties INNER JOIN
                                  Purchase ON Parties.PartyID = Purchase.VendorID ON PurchaseBody.PurchaseID = Purchase.PurchaseID LEFT OUTER JOIN
                                      (SELECT     PurReturn_1.PurReturnID, PurReturn_1.PurchaseID, PurReturn_1.EntryDate, PurReturn_1.GrossValue, PurReturn_1.Narration, PurReturn_1.VendorID, 
                                                               PurReturn_1.UserID, PurReturn_1.CashReceived, PurReturnBody.ProductID, PurReturnBody.Qty, PurReturnBody.Price, PurReturnBody.Discount, 
                                                               PurReturnBody.TotalValue, 0 as Multiplier
                                        FROM          PurReturn AS PurReturn_1 INNER JOIN
                                                               PurReturnBody ON PurReturn_1.PurReturnID = PurReturnBody.PurReturnID) AS PurReturn ON PurchaseBody.ProductID = PurReturn.ProductID AND 
                                  Purchase.PurchaseID = PurReturn.PurchaseID
Where Purchase.PurchaseID = (select PurchaseID from PurReturn where PurReturnID = {0})
            order by PurReturn.PurchaseID Desc
";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vPurRetID));
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

            string vSql = "Select COALESCE(MAX(Isnull(PurReturnID,0)),0) + 1  as PurReturnID From PurReturn ";


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

        public DataTable getPurchaseData(string vWhere)
        {
            string query = @"SELECT     Purchase.PurchaseID, Purchase.EntryDate, Purchase.VendorID, Parties.PartyName, Purchase.GrossValue, Purchase.Narration, Purchase.CashPaid, 
                      PurchaseBody.ProductID, Products.ProductName, PurchaseBody.Qty, PurchaseBody.Price, PurchaseBody.Discount, PurchaseBody.TotalValue, Products.UnitID, 
                      UnitsInfo.UnitTitle, 0 as Units
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
    }
}
