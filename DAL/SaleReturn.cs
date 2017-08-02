using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
    public class SaleReturn
    {
        public string connectionstring;
        public void InsertRecord(Objects.SaleReturn obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaleReturnInsert";

                cmd.Parameters.AddWithValue("@SaleReturnID", obj.SaleReturnID);
                cmd.Parameters.AddWithValue("@SaleID", obj.SaleID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@SpecialDisc", obj.SpecialDisc);
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
        public void InsertRecordBody(Objects.SaleRetBody obj)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaleRetBodyInsert";
                
                cmd.Parameters.AddWithValue("@SaleReturnID", obj.SaleReturnID);
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
        public void UpdateRecord(Objects.SaleReturn obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SaleReturnUpdate";

                cmd.Parameters.AddWithValue("@SaleReturnID", obj.SaleReturnID);
                cmd.Parameters.AddWithValue("@SaleID", obj.SaleID);
                cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
                cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
                cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
                cmd.Parameters.AddWithValue("@SpecialDisc", obj.SpecialDisc);
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
                cmd.CommandText = "SP_SaleReturnDelete";

                cmd.Parameters.AddWithValue("@SaleReturnID", vID);
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
                string vSql = "Delete From SaleRetBody Where SaleReturnID={0} ";

                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vID));

            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public DataTable getRecord(Int64 vSaleRetID)
        {
            string query = @"SELECT     Sale.SaleID,Isnull(SaleReturn.SaleReturnID,0) as SaleReturnID,Isnull(SaleReturn.SpecialDisc,0) as SpecialDisc, Sale.EntryDate AS SaleDate, Sale.CustomerID, Parties.PartyName, Employees.EmployeeName, (CASE WHEN SaleReturn.ProductID IS NULL 
                      THEN 'false' ELSE 'true' END) AS Selected, SaleBody.ProductID, Products.ProductName, ISNULL(SaleReturn.Qty, SaleBody.Qty) AS Qty, SaleBody.Cost, 
                      ISNULL(SaleReturn.Price, SaleBody.Price) AS Price, ISNULL(SaleReturn.Discount, SaleBody.Discount) AS Discount, ISNULL(SaleReturn.TotalValue, 
                      SaleBody.TotalValue) AS TotalValue, Products.UnitID, UnitsInfo.UnitTitle, 0 
                      AS Units,  SaleReturn.EntryDate, SaleReturn.GrossValue, SaleReturn.Narration, SaleReturn.CashPaid, SaleReturn.UserID
                FROM         Sale INNER JOIN
                                      SaleBody ON Sale.SaleID = SaleBody.SaleID INNER JOIN
                                      Parties ON Sale.CustomerID = Parties.PartyID INNER JOIN
                                      Products ON SaleBody.ProductID = Products.ProductID LEFT OUTER JOIN
                                          (SELECT     SaleReturn_1.SaleReturnID, SaleReturn_1.SaleID, SaleReturn_1.EntryDate, SaleReturn_1.SpecialDisc, SaleReturn_1.GrossValue, SaleReturn_1.Narration, 
                                                                   SaleReturn_1.CustomerID, SaleReturn_1.UserID, SaleReturn_1.CashPaid, SaleRetBody.ProductID, SaleRetBody.Qty, SaleRetBody.Price, 
                                                                   SaleRetBody.Discount, SaleRetBody.TotalValue,  0 as Multiplier
                                            FROM          SaleReturn AS SaleReturn_1 INNER JOIN
                                                                   SaleRetBody ON SaleReturn_1.SaleReturnID = SaleRetBody.SaleReturnID) AS SaleReturn ON SaleBody.ProductID = SaleReturn.ProductID AND 
                                      Sale.SaleID = SaleReturn.SaleID LEFT OUTER JOIN
                                      Employees ON Sale.SalesmanID = Employees.EmployeeID LEFT OUTER JOIN
                                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                WHERE     (Sale.SaleID =
                                          (SELECT     SaleID
                                            FROM          SaleReturn AS SaleReturn_2
                                            WHERE      (SaleReturnID = {0})))
                Order By SaleReturn.SaleReturnID Desc  ";
            DataSet ds = new DataSet();

            try
            {

                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vSaleRetID));
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

            string vSql = "Select COALESCE(MAX(Isnull(SaleReturnID,0)),0) + 1  as SaleReturnID From SaleReturn ";


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
        public DataTable getSaleData(string vWhere)
        {
            string query = @"SELECT     Sale.SaleID, Sale.EntryDate, Sale.CustomerID, Parties.PartyName, Sale.SalesmanID, Employees.EmployeeName, Sale.GrossValue, Sale.SpecialDisc, Sale.Narration, 
                      Sale.UserID, Sale.CashReceived, SaleBody.ProductID, Products.ProductName, SaleBody.Qty,SaleBody.Cost, SaleBody.Price, SaleBody.Discount, SaleBody.TotalValue, 
                      Products.UnitID, UnitsInfo.UnitTitle, 0 as Units
                FROM         Sale INNER JOIN
                      SaleBody ON Sale.SaleID = SaleBody.SaleID INNER JOIN
                      Parties ON Sale.CustomerID = Parties.PartyID INNER JOIN
                      Products ON SaleBody.ProductID = Products.ProductID LEFT OUTER JOIN
                      Employees ON Sale.SalesmanID = Employees.EmployeeID LEFT OUTER JOIN
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
