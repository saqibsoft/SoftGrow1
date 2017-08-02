using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
   public class Misc
    {
       public string connectionstring;       

       public DataTable getPartyFinal(Int64 vPartyID)
       {
           string query = @"SELECT  PartyID,Debit,Credit   
                        Where 1 = 1  and PartyID={0}";
           DataSet ds = new DataSet();

           try
           {
               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vPartyID));
               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public void TakeDBBackup()
       {
           try
           {
               string vPath = System.Environment.CurrentDirectory;
               string vFileName = RemoveSpecialCharacters(DateTime.Now.ToString("dd MMM yyyy")) + ".bak";
               string vLocation = vPath + "\\" + vFileName;

               string vSql = string.Format(@"BACKUP DATABASE SkyWater TO disk = '{0}'", vLocation);


               new Database(connectionstring).ExecuteNonQueryOnly(vSql);
           }
           catch (Exception exc)
           {
               
               throw exc;
           }

       }

       public string RemoveSpecialCharacters(string str)
       {
           StringBuilder sb = new StringBuilder();
           foreach (char c in str)
           {
               if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
               {
                   sb.Append(c);
               }
           }
           return sb.ToString();
       }

       public DataTable getVouchersForAdmin(string vWhere)
       {
           string query = @"Select  * From
                        (
                        SELECT     VoucherHeader.VoucherID as VoucherNo, VoucherHeader.VoucherType, VoucherHeader.VoucherDate, VoucherHeader.ProjectID, Projects.ProjectTitle, VoucherHeader.UserID, 
                                                Users.UserName, CASE WHEN SUM(VoucherBody.Debit) > 0 THEN SUM(VoucherBody.Debit) ELSE SUM(VoucherBody.Credit) 
                                                END AS Amount,Isnull(VoucherHeader.IsPosted,0) as Selected
                        FROM         VoucherHeader INNER JOIN
                                                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType INNER JOIN
                                                Users ON VoucherHeader.UserID = Users.UserID LEFT OUTER JOIN
                                                Projects ON VoucherHeader.ProjectID = Projects.ProjectID
                        GROUP BY VoucherHeader.VoucherID, VoucherHeader.VoucherType, VoucherHeader.VoucherDate, VoucherHeader.ProjectID, Projects.ProjectTitle, VoucherHeader.UserID, 
                                                Users.UserName, VoucherHeader.IsPosted                      
                        UNION ALL
                        SELECT     BankDeposits.DepositID AS VoucherNo, CASE WHEN Isnull(BankDeposits.IsCheque, 0) = 0 THEN 'BDV' ELSE 'CDV' END AS VoucherType, 
                                                BankDeposits.DepositDate AS VoucherDate, BankDeposits.ProjectID, Projects.ProjectTitle, BankDeposits.UserID, Users.UserName, BankDeposits.Amount,BankDeposits.IsPosted
                        FROM         BankDeposits INNER JOIN
                                                Projects ON BankDeposits.ProjectID = Projects.ProjectID INNER JOIN
                                                Users ON BankDeposits.UserID = Users.UserID
                        UNION ALL

                        SELECT     BankIssues.IssueID, 'BCI' AS VoucherTYpe,BankIssues.IssueDate, BankIssues.ProjectID, Projects.ProjectTitle, BankIssues.UserID, Users.UserName, 
                                                BankIssues.Amount, BankIssues.IsPosted
                        FROM         BankIssues INNER JOIN
                                                Projects ON BankIssues.ProjectID = Projects.ProjectID INNER JOIN
                                                Users ON BankIssues.UserID = Users.UserID                      
                        )tmpTable
                        Where 1=1 {0}
                        ";
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

       public DataTable getCurrentStock(Int64 vProductID)
       {
//           string query = @"Select Round(SUM(Qty),2) as Qty, Round(SUM(convert(numeric(12,5),Value)),2) as Value 
//                            From (
//                            select ProductID,OpeningStock as Qty,OpeningStockValue as Value from products
//                            Where ProductID={0}
//                            Union ALL
//                            select Productid,Qty,TotalValue from Purchasebody
//                            Where ProductID={0}
//                            Union ALL
//                            Select ProductID,Qty,Qty *Cost  from ProductionBody
//                            Where ProductID={0}
//                            Union ALL
//                            Select ProductID,-Qty,0  from Consumption
//                            Where ProductID={0}
//                            Union ALL
//                            Select ProductID,-Qty,0 from SaleBody
//                            Where ProductID={0} ) tmptable";
           string query = @"SELECT     tmptable.ProductID, SUM(tmptable.Qty) AS Qty,SUM(tmptable.LessQTY) AS LessQty, SUM(tmptable.Value) AS Value, Products.ProductName, Products.UnitID, UnitsInfo.UnitTitle
            FROM         (SELECT     ProductID, OpeningStock AS Qty, OpeningStockValue AS Value,0 as LessQTY
                        FROM         Products AS Products_1
                        Where Products_1.ProductID={0}
                        UNION ALL
                        SELECT     ProductID, Qty, TotalValue,0 as LessQTY
                        FROM         PurchaseBody
                        Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID, -Qty, -TotalValue,0 as LessQTY
                        FROM         PurReturnBody
                        Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID, Qty, Qty * Cost ,0 as LessQTY
                        FROM         ProductionBody
                        Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost 
                        FROM         Consumption
                        Where ProductID={0}
                        UNION ALL
SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         CustomerIssueBody
Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID,  Qty , Qty * isnull(Cost,0), 0 as LessQTY  
                                                FROM         CustomerReturnBody
Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID,  -Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         StockSampleBody
Where ProductID={0}
                        UNION ALL
                        SELECT     ProductID,  -Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         StockWastageBody
Where ProductID={0}
UNION ALL
                        SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost
                        FROM         SaleBody
                        Where ProductID={0}
                        UNION ALL
                        /* SELECT     ProductID,  Qty ,TotalValue
                        FROM         SaleRetBody */
                        SELECT    SaleRetBody.ProductID, SaleRetBody.Qty,0,0 as LessQTY -- SaleRetBody.Qty * SaleBody.Cost
                        FROM         SaleReturn INNER JOIN
                                              SaleRetBody ON SaleReturn.SaleReturnID = SaleRetBody.SaleReturnID INNER JOIN
                                              SaleBody ON SaleReturn.SaleID = SaleBody.SaleID AND SaleRetBody.ProductID = SaleBody.ProductID
                        Where SaleRetBody.ProductID={0}
) AS tmptable INNER JOIN
                      Products AS Products ON tmptable.ProductID = Products.ProductID INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
            Where 1=1 AND Products.ProductID={0}
            GROUP BY tmptable.ProductID, Products.ProductName, Products.UnitID, UnitsInfo.UnitTitle
            HAVING      (SUM(tmptable.Qty) > 0)";

           DataSet ds = new DataSet();

           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vProductID));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public decimal getOpeningStockValue(string vWhere)
       {
           string query = @"SELECT     isnull(sum(OpeningStockValue),0) AS Value
                        FROM         Products where 1=1 {0}";
           DataSet ds = new DataSet();
           try
           {
               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vWhere));
               return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public decimal getNetSalesValue(string vFrom, string vToDate)
       {
           string query = @"SELECT     dbo.FunGetNetSalesValue ('{0}','{1}')
                        ";
           DataSet ds = new DataSet();
           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vFrom, vToDate));
               return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public decimal getNetPurchaseValue(string vFrom, string vToDate)
       {
           string query = @"SELECT     dbo.FunGetNetPurchaseValue ('{0}','{1}')
                        ";
           DataSet ds = new DataSet();
           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vFrom, vToDate));
               return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

    }
}
