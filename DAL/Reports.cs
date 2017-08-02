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
   public class Reports
    {
       public string connectionstring;

       public DataTable getCompanyInfo()
       {
           string query = @"select CompanyName,ComapnyLogo,Phone,Address from CompanyInfo";
           DataSet ds = new DataSet();

           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(query);
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getCurrentStock(string vWhere)
       {
           string query = @"SELECT     tmptable.ProductID, SUM(tmptable.Qty) AS Qty,SUM(tmptable.LessQTY) AS LessQty, SUM(tmptable.Value) AS Value, Products.ProductName, Products.UnitID, UnitsInfo.UnitTitle
            FROM         (SELECT     ProductID, OpeningStock AS Qty, OpeningStockValue AS Value,0 as LessQTY
                        FROM         Products AS Products_1
                        UNION ALL
                        SELECT     ProductID, Qty, TotalValue,0 as LessQTY
                        FROM         PurchaseBody
                        UNION ALL
                        SELECT     ProductID, -Qty, -TotalValue,0 as LessQTY
                        FROM         PurReturnBody
                        UNION ALL
                        SELECT     ProductID, Qty, Qty * Cost ,0 as LessQTY
                        FROM         ProductionBody
                        UNION ALL
                        SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost 
                        FROM         Consumption
                        UNION ALL
                        SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         CustomerIssueBody
                        UNION ALL
                        SELECT     ProductID,  Qty , Qty * isnull(Cost,0), 0 as LessQTY -- -Qty * Cost
                                                FROM         CustomerReturnBody
                        UNION ALL
                        SELECT     ProductID,  -Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         StockSampleBody
                        UNION ALL
                        SELECT     ProductID,  -Qty , 0, Qty as LessQTY -- -Qty * Cost
                                                FROM         StockWastageBody
                        UNION ALL
                        SELECT     ProductID, - Qty , 0, Qty as LessQTY -- -Qty * Cost
                        FROM         SaleBody
                        UNION ALL
                        /* SELECT     ProductID,  Qty ,TotalValue
                        FROM         SaleRetBody */
                        SELECT    SaleRetBody.ProductID, SaleRetBody.Qty,0,0 as LessQTY -- SaleRetBody.Qty * SaleBody.Cost
                        FROM         SaleReturn INNER JOIN
                                              SaleRetBody ON SaleReturn.SaleReturnID = SaleRetBody.SaleReturnID INNER JOIN
                                              SaleBody ON SaleReturn.SaleID = SaleBody.SaleID AND SaleRetBody.ProductID = SaleBody.ProductID
) AS tmptable INNER JOIN
                      Products AS Products ON tmptable.ProductID = Products.ProductID INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
            Where 1=1 {0}
            GROUP BY tmptable.ProductID, Products.ProductName, Products.UnitID, UnitsInfo.UnitTitle
            HAVING      (SUM(tmptable.Qty) > 0)";
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

       public DataTable getProductList(string vWhere)
       {
           string query = @"SELECT     Products.ProductID, Products.ProductName, Products.CostPrice as PurchasePrice, Products.SalePrice, Products.IsRawMaterial as IsConsumable, 0 as IsSaleable, Products.UnitID, 
                      UnitsInfo.UnitTitle
                FROM         Products INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                Where 1=1 {0}";

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

       public DataTable getPartiesList(string vWhere)
       {
           string query = @"SELECT     PartyID, PartyName, CNICNo, ContactNo, Address, City, 0 as IsSupplier, 0 as IsCustomer
                FROM         Parties
                Where 1=1 {0}";

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
      
       public DataTable getBankAccList(string vWhere)
       {
           string query = @"SELECT     AccountChart.AccountNo, AccountChart.AccountType, BankAccounts.AccountTitle, BankAccounts.BranchName, BankAccounts.BranchCode, 
                                                  BankAccounts.BankName
                            FROM         BankAccounts INNER JOIN
                                                  AccountChart ON BankAccounts.AccountID = AccountChart.AccountNo
                                                        Where 1=1 {0}";
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

       public DataTable getAccountsList(string vWhere)
       {
           string query = @"SELECT     AccountNo, AccountType, AccountTitle
                            FROM         AccountChart
                            WHERE     1=1                       
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

       public DataTable getSaleInvoices(string vWhere)
       {
           string query = @"SELECT     Sale.SaleID, Sale.EntryDate, Sale.CustomerID, Parties.PartyName, Sale.GrossValue, Sale.SpecialDisc, Sale.Narration, Sale.CashReceived, SaleBody.ProductID, 
                      Products.ProductName, SaleBody.Qty, SaleBody.Price, SaleBody.Discount, SaleBody.TotalValue, Products.UnitID, UnitsInfo.UnitTitle,Parties.ContactNo, Parties.NTN, 
                      Parties.Address, Parties.CNICNo
FROM         Sale INNER JOIN
                      SaleBody ON Sale.SaleID = SaleBody.SaleID INNER JOIN
                      Parties ON Sale.CustomerID = Parties.PartyID INNER JOIN
                      Products ON SaleBody.ProductID = Products.ProductID LEFT OUTER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                Where 1=1  {0}";

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

       public DataTable getPrtyWiseSale(string vWhere)
       {
           string query = @"SELECT     Sale.SaleID AS InvoiceNo, Sale.EntryDate AS InvoiceDate, Parties.AccountID AS PartyID, dbo.FunGetSaleNarration(Sale.SaleID) + ' -- ' + Sale.Narration AS Description, 
                      Sale.GrossValue AS TotalSale, ISNULL(Sale.SpecialDisc, 0) AS SpecialDiscount, ISNULL(Sale.CashReceived, 0) AS CashReceived, Parties.PartyName, 
                      Sale.SalesmanID, Employees.EmployeeName,Parties.Address
FROM         Sale INNER JOIN
                      Parties ON Sale.CustomerID = Parties.PartyID LEFT OUTER JOIN
                      Employees ON Sale.SalesmanID = Employees.EmployeeID
WHERE     (1 = 1) {0}
ORDER BY InvoiceDate
                      
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

       public DataTable getPrtyWiseSaleRet(string vWhere)
       {
           string query = @"SELECT     SaleReturnID AS InvoiceNo, EntryDate AS InvoiceDate,Parties.AccountID as PartyID,Parties.PartyName,Parties.Address, dbo.FunGetSaleReturnNarration(SaleReturnID) + ' -- ' + Narration AS Description,isnull(CashPaid,0) AS CashPaid,  GrossValue as TotalSaleRet , Isnull(SpecialDisc,0) as SpecialDiscount
                                                    FROM         SaleReturn INNER JOIN
												  Parties ON SaleReturn.CustomerID = Parties.PartyID
                                                    Where 1=1 {0}                     
                            order by EntryDate 
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

       public DataTable getPrtyWisePur(string vWhere)
       {
           string query = @"SELECT     PurchaseID AS InvoiceNo, EntryDate AS InvoiceDate, Parties.AccountID as PartyID,Parties.PartyName, dbo.FunGetPurchaseNarration(PurchaseID) + ' -- ' + Narration AS Description, Isnull(CashPaid,0) AS CashPaid, GrossValue AS GrossPurchase,0 as Discount
                            FROM         Purchase INNER JOIN
												  Parties ON Purchase.VendorID = Parties.PartyID
                            Where 1=1 {0}
                        order by EntryDate
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

       public DataTable getPrtyWisePurRet(string vWhere)
       {
           string query = @"SELECT     PurReturnID AS InvoiceNo, EntryDate AS InvoiceDate,Parties.AccountID as PartyID,Parties.PartyName, dbo.FunGetPurReturnNarration(PurReturnID) + ' -- ' + Narration AS Description,GrossValue  AS GrossValue, isnull(CashReceived,0) AS CashReceived,0 as Discount
                            FROM         PurReturn INNER JOIN
												  Parties ON PurReturn.VendorID = Parties.PartyID
                            Where 1=1 {0}                           
                        order by EntryDate
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

       public DataTable getSaleRetInvoices(string vWhere)
       {
           string query = @"SELECT     SaleReturn.SaleReturnID as SaleID, SaleReturn.EntryDate, SaleReturn.CustomerID, Parties.PartyName, SaleReturn.GrossValue, SaleReturn.SpecialDisc, SaleReturn.Narration, SaleReturn.CashPaid as CashReceived, SaleRetBody.ProductID, 
                      Products.ProductName, SaleRetBody.Qty, SaleRetBody.Price, SaleRetBody.Discount, SaleRetBody.TotalValue, Products.UnitID, UnitsInfo.UnitTitle
                FROM         SaleReturn INNER JOIN
                                      SaleRetBody ON SaleReturn.SaleReturnID = SaleRetBody.SaleReturnID INNER JOIN
                                      Parties ON SaleReturn.CustomerID = Parties.PartyID INNER JOIN
                                      Products ON SaleRetBody.ProductID = Products.ProductID LEFT OUTER JOIN
                                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
                Where 1=1  {0}";

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

       public DataTable getPurchaseInvoices(string vWhere)
       {
           string query = @"SELECT     Purchase.PurchaseID, Purchase.EntryDate, Purchase.VendorID, Parties.PartyName, Purchase.GrossValue, Purchase.Narration, Purchase.CashPaid, 
PurchaseBody.ProductID, Products.ProductName, PurchaseBody.Qty,
PurchaseBody.Price, PurchaseBody.Discount,
PurchaseBody.TotalValue , Products.UnitID, 
UnitsInfo.UnitTitle
FROM         Purchase INNER JOIN
PurchaseBody ON Purchase.PurchaseID = PurchaseBody.PurchaseID INNER JOIN
Parties ON Purchase.VendorID = Parties.PartyID INNER JOIN
Products ON PurchaseBody.ProductID = Products.ProductID LEFT OUTER JOIN
UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
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

       public DataTable getPurchaseReturnInvoices(string vWhere)
       {
           string query = @"SELECT     PurReturn.PurReturnID as PurchaseID, PurReturn.EntryDate, PurReturn.VendorID, Parties.PartyName, PurReturn.GrossValue,  PurReturn.Narration, PurReturn.CashReceived as CashPaid, PurReturnBody.ProductID, 
                      Products.ProductName, PurReturnBody.Qty, PurReturnBody.Price, PurReturnBody.Discount, PurReturnBody.TotalValue, Products.UnitID, UnitsInfo.UnitTitle
                        FROM         PurReturn INNER JOIN
                                              PurReturnBody ON PurReturn.PurReturnID = PurReturnBody.PurReturnID INNER JOIN
                                              Parties ON PurReturn.VendorID = Parties.PartyID INNER JOIN
                                              Products ON PurReturnBody.ProductID = Products.ProductID LEFT OUTER JOIN
                                              UnitsInfo ON Products.UnitID = UnitsInfo.UnitID
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
       
       // Account Ledger
       public DataTable getPartyLedger(string vPartyID,string vFromDate,string vToDate)
       {
           string query = @"
select * from
(

Select '' AS InvoiceNo, NULL AS InvoiceDate, 'Op' AS DocType, AccountNo, 'Opening Balance' AS Description, OpeningDebit as Debit, OpeningCredit as Credti
FROM         AccountChart WHERE AccountNo={0}
UNION ALL
Select 0 AS InvoiceNo, NULL AS InvoiceDate, 'Prev' AS DocType, PartyID, 'Previous Balance' AS Description, SUM(Debit) as Debit, SUM(Credit) as Credti from 
                            (                            
                            SELECT     Parties.AccountID AS PartyID, Purchase.CashPaid AS Debit, Purchase.GrossValue AS Credit
							FROM         Purchase INNER JOIN
												  Parties ON Purchase.VendorID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, Purchase.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})
                            UNION ALL
                            SELECT     '100000' AS PartyID, 0 AS Debit, Purchase.CashPaid AS Credit
							FROM         Purchase INNER JOIN
												  Parties ON Purchase.VendorID = Parties.PartyID
							WHERE   isnull(Purchase.CashPaid,0) > 0 and  (CONVERT(datetime, CONVERT(varchar, Purchase.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})												  
                            UNION ALL
                            
                            SELECT     Parties.AccountID AS PartyID, PurReturn.GrossValue AS Debit, ISNULL(PurReturn.CashReceived, 0) AS Credit
							FROM         PurReturn INNER JOIN
												  Parties ON PurReturn.VendorID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, PurReturn.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})
                            UNION ALL
                            SELECT     '100000' AS PartyID, ISNULL(PurReturn.CashReceived, 0) AS Debit, 0 AS Credit
							FROM         PurReturn INNER JOIN
												  Parties ON PurReturn.VendorID = Parties.PartyID
							WHERE  ISNULL(PurReturn.CashReceived, 0) > 0 and   (CONVERT(datetime, CONVERT(varchar, PurReturn.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})

                            Union ALL

SELECT     Parties.AccountID AS PartyID, 0  AS Debit, CustomerIssue.SecurityDeposit AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, CustomerIssue.IssueDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})

UNION ALL
SELECT     '100000' AS PartyID, CustomerIssue.SecurityDeposit  AS Debit, 0 AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, CustomerIssue.IssueDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})

UNION ALL
SELECT     Parties.AccountID AS PartyID, CustomerReturn.SecurityReturn  AS Debit, 0 AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, CustomerReturn.ReturnDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})

UNION ALL
SELECT     '100000' AS PartyID, 0  AS Debit, CustomerReturn.SecurityReturn AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, CustomerReturn.ReturnDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})
UNION ALL

SELECT     Parties.AccountID AS PartyID,0 as Debit, RecoveryBody.AmountRecoverd as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
WHERE     (CONVERT(datetime, CONVERT(varchar, Recovery.RecoveryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})                      

UNION ALL

SELECT     '100000' AS PartyID,RecoveryBody.AmountRecoverd as Debit, 0 as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
WHERE     (CONVERT(datetime, CONVERT(varchar, Recovery.RecoveryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})                      


UNION ALL

                            SELECT     Parties.AccountID AS PartyID, Sale.GrossValue - Sale.SpecialDisc AS Debit, Sale.CashReceived AS Credit
							FROM         Sale INNER JOIN
												  Parties ON Sale.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, Sale.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})
												  
                            UNION ALL

                            SELECT     '100000' AS PartyID, Sale.CashReceived AS Debit, 0 AS Credit
							FROM         Sale INNER JOIN
												  Parties ON Sale.CustomerID = Parties.PartyID
							WHERE   isnull(Sale.CashReceived,0) > 0 and  (CONVERT(datetime, CONVERT(varchar, Sale.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (100000 = {0})
												  
                            UNION ALL
                            
                            SELECT     Parties.AccountID AS PartyID, SaleReturn.CashPaid AS Debit, SaleReturn.GrossValue - SaleReturn.SpecialDisc AS Credit
							FROM         SaleReturn INNER JOIN
												  Parties ON SaleReturn.CustomerID = Parties.PartyID
							WHERE     (CONVERT(datetime, CONVERT(varchar, SaleReturn.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) AND 
												  (Parties.AccountID = {0})
							UNION ALL
                            SELECT     '100000' AS PartyID, 0 AS Debit, SaleReturn.CashPaid AS Credit
							FROM         SaleReturn INNER JOIN
												  Parties ON SaleReturn.CustomerID = Parties.PartyID
							WHERE  isnull(SaleReturn.CashPaid,0) > 0 and   (CONVERT(datetime, CONVERT(varchar, SaleReturn.EntryDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '1/1/2000 00:00:00', 102), 1))) AND 
												  (100000 = {0})
									
UNION ALL			  
							SELECT     VoucherBody.AccountNo, VoucherBody.Debit, VoucherBody.Credit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherBody.AccountNo={0} AND (CONVERT(datetime, CONVERT(varchar, VoucherHeader.VoucherDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))					  
					        UNION ALL
                            SELECT     '100000' as AccountNo, VoucherBody.Credit, VoucherBody.Debit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherHeader.VoucherType='CPV' and 100000={0} AND (CONVERT(datetime, CONVERT(varchar, VoucherHeader.VoucherDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))					  
					        UNION ALL
					        SELECT     '100000' as AccountNo, VoucherBody.Credit, VoucherBody.Debit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherHeader.VoucherType='CRV' and 100000={0} AND (CONVERT(datetime, CONVERT(varchar, VoucherHeader.VoucherDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))					  

					        UNION ALL
					        
					        SELECT     BankDeposits.BankAccountNo, BankDeposits.Amount AS Debit,  0 AS Credit
                        FROM         
                                              BankDeposits  INNER JOIN
                                              AccountChart ON BankDeposits.BankAccountNo = AccountChart.AccountNo
                        Where BankDeposits.BankAccountNo={0} AND (convert(datetime,Convert(varchar,BankDeposits.DepositDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))))    
                        
                        
                        UNION ALL
                        SELECT      BankDeposits.AccountNo, 0 AS Debit, BankDeposits.Amount AS Credit
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.AccountNo = AccountChart.AccountNo
                        WHERE   BankDeposits.AccountNo={0} AND   (CONVERT(datetime, CONVERT(varchar, BankDeposits.DepositDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))  
						
						UNION ALL
						
						SELECT     BankIssues.BankAccountNo, 0 AS Debit,  BankIssues.Amount AS Credit
                        FROM BankIssues  INNER JOIN
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        WHERE   BankIssues.BankAccountNo={0} AND   (CONVERT(datetime, CONVERT(varchar, BankIssues.IssueDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))      
                        UNION ALL
                        SELECT     '200000', 0 AS Debit,  BankIssues.WHTAmount AS Credit
                        FROM BankIssues  INNER JOIN
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        WHERE ISNULL(BankIssues.WHTAmount,0) > 0 and  200000={0} AND   (CONVERT(datetime, CONVERT(varchar, BankIssues.IssueDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '1/1/2000 00:00:00', 102), 1)))      
                        UNION ALL
                        
                        SELECT     BankIssueBody.AccountNo, BankIssueBody.Amount AS Debit, 0 AS Credit
                        FROM         BankIssueBody INNER JOIN
                                              BankIssues ON BankIssueBody.IssueID = BankIssues.IssueID INNER JOIN
                                              AccountChart ON BankIssueBody.AccountNo = AccountChart.AccountNo                                                   
                        WHERE  BankIssueBody.AccountNo={0} AND   (CONVERT(datetime, CONVERT(varchar, BankIssues.IssueDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1))) 
					      
UNION ALL 

SELECT   '#SalExpenseAcc' ,  TotalSalary as Debit, 0 as Credit
FROM         Salary
WHERE  '#SalExpenseAcc'={0} AND   (CONVERT(datetime, CONVERT(varchar, GenerateDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))   
UNION ALL
SELECT     Employees.AccountNo,0 as Debit, SalaryBody.NetSalary as Credit
FROM         SalaryBody INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID INNER JOIN
                      Salary ON SalaryBody.SalaryID = Salary.SalaryID
WHERE  Employees.AccountNo={0} AND   (CONVERT(datetime, CONVERT(varchar, Salary.GenerateDate, 1)) < CONVERT(Datetime, CONVERT(varchar, CONVERT(Datetime, '{1} 00:00:00', 102), 1)))   

                            )tmpTable
                            Group by PartyID

                            Union All

                            SELECT     PurchaseID AS InvoiceNo, EntryDate AS InvoiceDate, 'PUR' AS DocType,Parties.AccountID as PartyID, dbo.FunGetPurchaseNarration(PurchaseID) + ' -- ' + Narration AS Description, CashPaid AS Debit, GrossValue AS Credit
                            FROM         Purchase INNER JOIN
												  Parties ON Purchase.VendorID = Parties.PartyID
                            Where Parties.AccountID={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            UNION ALL
                            SELECT     PurchaseID AS InvoiceNo, EntryDate AS InvoiceDate, 'PUR' AS DocType,'100000' as PartyID, dbo.FunGetPurchaseNarration(PurchaseID) + ' -- ' + Narration AS Description, 0 AS Debit, CashPaid AS Credit
                            FROM         Purchase INNER JOIN
												  Parties ON Purchase.VendorID = Parties.PartyID
                            Where isnull(CashPaid,0) > 0 and  100000={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            Union ALL
                            SELECT     PurReturnID AS InvoiceNo, EntryDate AS InvoiceDate, 'PUR-R' AS DocType,Parties.AccountID as PartyID, dbo.FunGetPurReturnNarration(PurReturnID) + ' -- ' + Narration AS Description,GrossValue  AS Debit, CashReceived AS Credit
                            FROM         PurReturn INNER JOIN
												  Parties ON PurReturn.VendorID = Parties.PartyID
                            Where Parties.AccountID={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            UNION ALL
                            SELECT     PurReturnID AS InvoiceNo, EntryDate AS InvoiceDate, 'PUR-R' AS DocType,'100000' as PartyID, dbo.FunGetPurReturnNarration(PurReturnID) + ' -- ' + Narration AS Description,CashReceived AS Debit, 0 AS Credit
                            FROM         PurReturn INNER JOIN
												  Parties ON PurReturn.VendorID = Parties.PartyID
                            Where isnull(CashReceived,0) > 0 and '100000'={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            UNION ALL
SELECT   CustomerIssue.IssueID AS InvoiceNo,CustomerIssue.IssueDate,'CI' AS DocType,  Parties.AccountID AS PartyID,CustomerIssue.Remarks, 0  AS Debit, CustomerIssue.SecurityDeposit AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID
							WHERE     convert(datetime,Convert(varchar,CustomerIssue.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND
												  (Parties.AccountID = {0})

UNION ALL
SELECT    CustomerIssue.IssueID AS InvoiceNo,CustomerIssue.IssueDate,'CI' AS DocType, '100000' AS PartyID,CustomerIssue.Remarks, CustomerIssue.SecurityDeposit  AS Debit, 0 AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID
							WHERE     convert(datetime,Convert(varchar,CustomerIssue.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND 
							
												  (100000 = {0})

UNION ALL
SELECT     CustomerReturn.ReturnID AS InvoiceNo,CustomerReturn.ReturnDate,'CR' AS DocType, Parties.AccountID AS PartyID,CustomerReturn.Remarks, CustomerReturn.SecurityReturn  AS Debit, 0 AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
							WHERE     convert(datetime,Convert(varchar,CustomerReturn.ReturnDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND 
							
												  (Parties.AccountID = {0})

UNION ALL
SELECT     CustomerReturn.ReturnID AS InvoiceNo,CustomerReturn.ReturnDate,'CR' AS DocType, '100000' AS PartyID,CustomerReturn.Remarks,  0  AS Debit, CustomerReturn.SecurityReturn AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
							WHERE     convert(datetime,Convert(varchar,CustomerReturn.ReturnDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND 
							
												  (100000 = {0})
UNION ALL

SELECT     Recovery.RecoveryID AS InvoiceNo,Recovery.RecoveryDate,'REC' AS DocType, Parties.AccountID AS PartyID,Recovery.Remarks, 0 as Debit, RecoveryBody.AmountRecoverd as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
WHERE     convert(datetime,Convert(varchar,Recovery.RecoveryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND 

												  (Parties.AccountID = {0})                      

UNION ALL

SELECT     Recovery.RecoveryID AS InvoiceNo,Recovery.RecoveryDate,'REC' AS DocType, '100000' AS PartyID,Recovery.Remarks,RecoveryBody.AmountRecoverd as Debit, 0 as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
WHERE     convert(datetime,Convert(varchar,Recovery.RecoveryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1))) AND 
												  (100000 = {0})                      




UNION ALL
                            SELECT     SaleID AS InvoiceNo, EntryDate AS InvoiceDate, 'SAL' AS DocType,Parties.AccountID as PartyID, dbo.FunGetSaleNarration(SaleID) + ' -- ' + Narration AS Description, GrossValue - SpecialDisc AS Debit, CashReceived as Credit
                            FROM         Sale INNER JOIN
												  Parties ON Sale.CustomerID = Parties.PartyID
                            Where Parties.AccountID={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            UNION ALL
                            SELECT     SaleID AS InvoiceNo, EntryDate AS InvoiceDate, 'SAL' AS DocType,'100000' as PartyID, dbo.FunGetSaleNarration(SaleID) + ' -- ' + Narration AS Description, CashReceived as Debit, 0 as Credit
                            FROM         Sale INNER JOIN
												  Parties ON Sale.CustomerID = Parties.PartyID
                            Where isnull(CashReceived,0) > 0 and 100000={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))

                           UNION ALL
                           SELECT     SaleReturnID AS InvoiceNo, EntryDate AS InvoiceDate, 'SAL-R' AS DocType,Parties.AccountID as PartyID, dbo.FunGetSaleReturnNarration(SaleReturnID) + ' -- ' + Narration AS Description,CashPaid AS Debit,  GrossValue - SpecialDisc as Credit
                                                    FROM         SaleReturn INNER JOIN
												  Parties ON SaleReturn.CustomerID = Parties.PartyID
                                                    Where Parties.AccountID={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                           UNION ALL
                            SELECT     SaleReturnID AS InvoiceNo, EntryDate AS InvoiceDate, 'SAL-R' AS DocType,'100000' as PartyID, dbo.FunGetSaleReturnNarration(SaleReturnID) + ' -- ' + Narration AS Description,0 AS Debit,  CashPaid as Credit
                                                    FROM         SaleReturn INNER JOIN
												  Parties ON SaleReturn.CustomerID = Parties.PartyID
                             Where isnull(CashPaid,0) > 0 and  100000={0} AND convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            UNION ALL
                           
                            SELECT     VoucherHeader.VoucherID,VoucherHeader.VoucherDate, VoucherHeader.VoucherType, VoucherBody.AccountNo, VoucherBody.Remarks, 
	                        VoucherBody.Debit, VoucherBody.Credit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherBody.AccountNo={0} AND convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
					        
					        UNION ALL

                            SELECT     VoucherHeader.VoucherID,VoucherHeader.VoucherDate, VoucherHeader.VoucherType, '100000'  as AccountNo, VoucherBody.Remarks, 
	                        VoucherBody.Credit, VoucherBody.Debit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherHeader.VoucherType='CPV' and 100000={0} AND convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))					        
					        UNION ALL
					        SELECT     VoucherHeader.VoucherID,VoucherHeader.VoucherDate, VoucherHeader.VoucherType,  '100000'  as AccountNo, VoucherBody.Remarks, 
	                        VoucherBody.Credit, VoucherBody.Debit
							FROM         VoucherHeader INNER JOIN
			                VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType 
					        Where VoucherHeader.VoucherType='CRV' and 100000={0} AND convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))					        
					        UNION ALL
					        
					        SELECT     BankDeposits.DepositID, BankDeposits.DepositDate, CASE WHEN Isnull(BankDeposits.IsCheque, 0) 
                                              = 0 THEN 'BDV' ELSE 'CDV' END AS InvType, BankDeposits.BankAccountNo,'Bank Deposit;' + BankDeposits.Narration, BankDeposits.Amount AS Debit, 
                                              0 AS Credit
                        FROM         
                                              BankDeposits  INNER JOIN
                                              AccountChart ON BankDeposits.BankAccountNo = AccountChart.AccountNo
                        Where BankDeposits.BankAccountNo ={0} AND  convert(datetime,Convert(varchar,BankDeposits.DepositDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))    
                        
                        
                        UNION ALL
                        SELECT     BankDeposits.DepositID, BankDeposits.DepositDate, CASE WHEN Isnull(BankDeposits.IsCheque, 0) = 0 THEN 'BDV' ELSE 'CDV' END AS InvType, 
                                              BankDeposits.AccountNo,'Bank Deposit;' + BankDeposits.Narration, 0 AS Debit, BankDeposits.Amount AS Credit
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.AccountNo = AccountChart.AccountNo
                        Where BankDeposits.AccountNo ={0} AND  convert(datetime,Convert(varchar,BankDeposits.DepositDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))    
						
						UNION ALL
						
						SELECT     BankIssues.IssueID, BankIssues.IssueDate, 'BCI' AS InvType, BankIssues.BankAccountNo, BankIssues.Narration, 0 AS Debit, 
                                              BankIssues.Amount AS Credit
                        FROM BankIssues  INNER JOIN
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        WHERE   BankIssues.BankAccountNo={0} AND   convert(datetime,Convert(varchar,BankIssues.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))    
                        
                        UNION ALL
                        SELECT     BankIssues.IssueID, BankIssues.IssueDate, 'BCI' AS InvType, '200000' as AccountNo, BankIssues.Narration, 0 AS Debit, 
                                              BankIssues.WHTAmount AS Credit
                        FROM BankIssues  INNER JOIN
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
                        WHERE ISNULL(BankIssues.WHTAmount,0) > 0 and  200000={0} AND   convert(datetime,Convert(varchar,BankIssues.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))                            
                        UNION ALL

                        SELECT     BankIssues.IssueID, BankIssues.IssueDate, 'BCI' AS InvType, BankIssueBody.AccountNo,BankIssues.Narration, 
                                              BankIssueBody.Amount AS Debit, 0 AS Credit
                        FROM         BankIssueBody INNER JOIN
                                              BankIssues ON BankIssueBody.IssueID = BankIssues.IssueID INNER JOIN
                                              AccountChart ON BankIssueBody.AccountNo = AccountChart.AccountNo                                                   
 
                       WHERE  BankIssueBody.AccountNo={0} AND   BankIssues.IssueDate Between CONVERT(DATETIME, '{1} 00:00:00', 102) AND CONVERT(DATETIME, '{2} 00:00:00', 102)
UNION ALL
SELECT    StockWastage.WasteID, StockWastage.EntryDate, 'WST' , '#ProductWastage' AS AccountNo,   StockWastage.Remarks, StockWastageBody.Qty * StockWastageBody.Cost AS Debit, 0 AS Credit
                     
FROM         StockWastageBody INNER JOIN
                      StockWastage ON StockWastageBody.WasteID = StockWastage.WasteID
WHERE '#ProductWastage'={0} AND   convert(datetime,Convert(varchar,StockWastage.EntryDate,1)) Between CONVERT(DATETIME, '{1} 00:00:00', 102) AND CONVERT(DATETIME, '{2} 00:00:00', 102)
             

                      
UNION ALL

SELECT   StockSamples.SampleID, StockSamples.IssueDate,'SAM',  '#SampleIssuance' AS AccountNo,   StockSamples.Remarks,  StockSampleBody.Qty * StockSampleBody.Cost AS Debit, 0 AS Credit
                    
FROM         StockSamples INNER JOIN
                      StockSampleBody ON StockSamples.SampleID = StockSampleBody.SampleID    
WHERE '#SampleIssuance'={0} AND   convert(datetime,Convert(varchar,StockSamples.IssueDate,1)) Between CONVERT(DATETIME, '{1} 00:00:00', 102) AND CONVERT(DATETIME, '{2} 00:00:00', 102)
             

UNION ALL

SELECT  SalaryID,GenerateDate,'SV', '#SalExpenseAcc' ,Remarks,  TotalSalary as Debit, 0 as Credit
FROM         Salary
WHERE  '#SalExpenseAcc'={0} AND  convert(datetime,Convert(varchar,  GenerateDate,1)) Between CONVERT(DATETIME, '{1} 00:00:00', 102) AND CONVERT(DATETIME, '{2} 00:00:00', 102)
UNION ALL

SELECT    Salary.SalaryID,Salary.GenerateDate,'SV', Employees.AccountNo,Salary.Remarks,0 as Debit, SalaryBody.NetSalary as Credit
FROM         SalaryBody INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID INNER JOIN
                      Salary ON SalaryBody.SalaryID = Salary.SalaryID
WHERE  Employees.AccountNo={0} AND   convert(datetime,Convert(varchar,Salary.GenerateDate,1)) Between CONVERT(DATETIME, '{1} 00:00:00', 102) AND CONVERT(DATETIME, '{2} 00:00:00', 102)

)tmTable
 order by InvoiceDate ";

           DataSet ds = new DataSet();

           try
           {
               Settings obj = new Settings();
               obj.connectionstring = connectionstring;

               query = query.Replace("#SalExpenseAcc", obj.GetSettingValue(Settings.ProSettings.SalaryExpAcc));
               query = query.Replace("#SampleIssuance", obj.GetSettingValue(Settings.ProSettings.SampleIssuance));
               query = query.Replace("#ProductWastage", obj.GetSettingValue(Settings.ProSettings.ProductWastage));


               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vPartyID,vFromDate,vToDate));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       // Party Receivable/Payable
       public DataTable getPartiesRecPay(string vTillDate, bool vRec)
       {
           string query = @"Select AccountNo,AccountTitle,#Column as Amount from 
(
SELECT     AccountChart.AccountType, AccountChart.AccountNo, AccountChart.AccountTitle, 
CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) + SUM(isnull(AllBal.Debit,0))) - (ISNULL(AccountChart.OpeningCredit, 0) + SUM(isnull(AllBal.Credit,0))) > 0 
      THEN (ISNULL(AccountChart.OpeningDebit, 0) + SUM(isnull(AllBal.Debit,0))) - (ISNULL(AccountChart.OpeningCredit, 0) + SUM(isnull(AllBal.Credit,0))) 
      ELSE 0 END AS Debit, 
CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) + SUM(isnull(AllBal.Debit,0))) - (ISNULL(AccountChart.OpeningCredit, 0) + SUM(isnull(AllBal.Credit,0)))  < 0 
     THEN ABS((ISNULL(AccountChart.OpeningDebit, 0) + SUM(isnull(AllBal.Debit,0))) - (ISNULL(AccountChart.OpeningCredit, 0) + SUM(isnull(AllBal.Credit,0)))) 
     ELSE 0 END AS Credit                    
                     
FROM         AccountChart LEFT OUTER JOIN
                          (SELECT     AccountID, SUM(Debit) AS Debit, SUM(Credit) AS Credit
                            FROM          (SELECT     Parties.AccountID, Purchase.EntryDate AS InvoiceDate, Purchase.CashPaid AS Debit, Purchase.GrossValue AS Credit
                                                    FROM          Purchase INNER JOIN
                                                                           Parties ON Purchase.VendorID = Parties.PartyID
                                                    UNION ALL
                                                    SELECT     Parties_3.AccountID, PurReturn.EntryDate AS InvoiceDate, PurReturn.GrossValue AS Debit, PurReturn.CashReceived AS Credit
                                                    FROM         PurReturn INNER JOIN
                                                                          Parties AS Parties_3 ON PurReturn.VendorID = Parties_3.PartyID
                                                    UNION ALL
SELECT     Parties.AccountID ,CustomerIssue.IssueDate, 0  AS Debit, CustomerIssue.SecurityDeposit AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID							
UNION ALL
SELECT     '100000' ,CustomerIssue.IssueDate, CustomerIssue.SecurityDeposit  AS Debit, 0 AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID							
UNION ALL
SELECT     Parties.AccountID ,CustomerReturn.ReturnDate, CustomerReturn.SecurityReturn  AS Debit, 0 AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
UNION ALL							
SELECT     '100000' AS PartyID,CustomerReturn.ReturnDate, 0  AS Debit, CustomerReturn.SecurityReturn AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID							
UNION ALL
SELECT     Parties.AccountID AS PartyID,Recovery.RecoveryDate, 0 as Debit, RecoveryBody.AmountRecoverd as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
UNION ALL
SELECT     '100000' AS PartyID,Recovery.RecoveryDate,RecoveryBody.AmountRecoverd as Debit, 0 as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
UNION ALL
                                                    SELECT     Parties_2.AccountID, Sale.EntryDate AS InvoiceDate, Sale.GrossValue - Sale.SpecialDisc AS Debit, Sale.CashReceived AS Credit
                                                    FROM         Sale INNER JOIN
                                                                          Parties AS Parties_2 ON Sale.CustomerID = Parties_2.PartyID
                                                    UNION ALL
                                                    SELECT     Parties_1.AccountID, SaleReturn.EntryDate AS InvoiceDate, SaleReturn.CashPaid AS Debit, 
                                                                          SaleReturn.GrossValue - SaleReturn.SpecialDisc AS Credit
                                                    FROM         SaleReturn INNER JOIN
                                                                          Parties AS Parties_1 ON SaleReturn.CustomerID = Parties_1.PartyID
                                                    UNION ALL
                                                    SELECT     VoucherBody.AccountNo, VoucherHeader.VoucherDate, VoucherBody.Debit, VoucherBody.Credit
                                                    FROM         VoucherHeader INNER JOIN
                                                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                                                    UNION ALL
                                                    SELECT     BankDeposits.BankAccountNo, BankDeposits.DepositDate, BankDeposits.Amount AS Debit, 0 AS Credit
                                                    FROM         BankDeposits INNER JOIN
                                                                          AccountChart AS AccountChart_4 ON BankDeposits.BankAccountNo = AccountChart_4.AccountNo
                                                    UNION ALL
                                                    SELECT     BankDeposits_1.AccountNo, BankDeposits_1.DepositDate, 0 AS Debit, BankDeposits_1.Amount AS Credit
                                                    FROM         BankDeposits AS BankDeposits_1 INNER JOIN
                                                                          AccountChart AS AccountChart_3 ON BankDeposits_1.BankAccountNo = AccountChart_3.AccountNo
                                                    UNION ALL
                                                    SELECT     BankIssues.BankAccountNo, BankIssues.IssueDate, 0 AS Debit, BankIssues.Amount AS Credit
                                                    FROM         BankIssues INNER JOIN
                                                                          AccountChart AS AccountChart_2 ON BankIssues.BankAccountNo = AccountChart_2.AccountNo
                                                    UNION ALL
                                                    SELECT     BankIssueBody.AccountNo, BankIssues_1.IssueDate, BankIssueBody.Amount AS Debit, 0 AS Credit
                                                    FROM         BankIssueBody INNER JOIN
                                                                          BankIssues AS BankIssues_1 ON BankIssueBody.IssueID = BankIssues_1.IssueID INNER JOIN
                                                                          AccountChart AS AccountChart_1 ON BankIssueBody.AccountNo = AccountChart_1.AccountNo

UNION ALL
SELECT  '#SalExpenseAcc',GenerateDate,TotalSalary as Debit, 0 as Credit
FROM         Salary     
UNION ALL
SELECT    Employees.AccountNo,Salary.GenerateDate,0 as Debit, SalaryBody.NetSalary as Credit
FROM         SalaryBody INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID INNER JOIN
                      Salary ON SalaryBody.SalaryID = Salary.SalaryID 

) AS tmTable
                            WHERE  (convert(datetime,Convert(varchar,InvoiceDate,1))  BETWEEN CONVERT(DATETIME, '1/1/2000 00:00:00', 102) AND CONVERT(DATETIME, '{0} 00:00:00', 102))
                            GROUP BY AccountID) AS AllBal ON AccountChart.AccountNo = AllBal.AccountID
where AccountChart.IsParty=1                             
Group By AccountChart.AccountType, AccountChart.AccountNo, AccountChart.AccountTitle,AccountChart.OpeningCredit,AccountChart.OpeningDebit
)tmpTable
Where 1=1 {1}
";

            string subQuery = string.Empty;
            if (vRec == true)
            { subQuery = " AND Debit > 0 ";
            query = query.Replace("#Column", "Debit");
            }
            else
            { subQuery = " AND Credit > 0 ";
            query = query.Replace("#Column", "Credit");
            }

           DataSet ds = new DataSet();

           try           
           {
               Settings obj = new Settings();
               obj.connectionstring = connectionstring;

               query = query.Replace("#SalExpenseAcc", obj.GetSettingValue(Settings.ProSettings.SalaryExpAcc));


               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vTillDate,subQuery));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getCashBook(string vFromDate, string vToDate)
       {
           string query = @"select * from
(
Select AllData.InvoiceNo,AllData.InvoiceType,AllData.AccountNo,Isnull(Parties.PartyName, AccountChart.AccountTitle) as AccountTitle,AllData.InvoiceDate,AllData.Remarks,AllData.Received,AllData.Paid From (
            Select 0 as InvoiceNo, 'OP' as InvoiceType,AccountNo,Null as InvoiceDate, 'Regesterd Opening' as Remarks,isnull(OpeningDebit,0) as Received,Isnull(OpeningCredit,0) as Paid from AccountChart Where AccountNo='100000'
            Union All
            --Opening Starts
            Select 0 as InvoiceNo,'Prev' as InvoiceType,'100000' as AccountNo, Null as InvoiceDate, 'Previous Balance' as Remarks,Isnull(SUM(Debit),0) as Received,Isnull(SUM(Credit),0) as Paid 
            From (
            Select 0 as Debit,CashPaid as Credit from Purchase
            Where (convert(datetime,Convert(varchar,EntryDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            UNION ALL
            Select CashReceived as Debit,0 as Credit from PurReturn
            Where (convert(datetime,Convert(varchar,EntryDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            Union All
Select SecurityDeposit,0 from CustomerIssue
            Where (convert(datetime,Convert(varchar,IssueDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
UNION ALL
Select 0,SecurityReturn from CustomerReturn
            Where (convert(datetime,Convert(varchar,ReturnDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))            
UNION ALL
Select TotalRecovery,0 from Recovery
            Where (convert(datetime,Convert(varchar,RecoveryDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))                        
Union All
            Select CashReceived,0 from Sale
            Where (convert(datetime,Convert(varchar,EntryDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            UNION ALL
            Select 0,CashPaid from SaleReturn
            Where (convert(datetime,Convert(varchar,EntryDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            UNION ALL
            SELECT      0 AS Debit, BankDeposits.Amount AS Credit
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.AccountNo = AccountChart.AccountNo
                        Where BankDeposits.AccountNo ='100000' AND  (convert(datetime,Convert(varchar,BankDeposits.DepositDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
UNION ALL						
SELECT      BankIssueBody.Amount as Debit , 0 as Credit
FROM         BankIssues INNER JOIN
                      BankIssueBody ON BankIssues.IssueID = BankIssueBody.IssueID INNER JOIN
                      AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
Where BankIssueBody.AccountNo =100000 AND  (convert(datetime,Convert(varchar,BankIssues.IssueDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
						
            Union All
            SELECT     VoucherBody.Credit ,VoucherBody.Debit 
            FROM         VoucherBody INNER JOIN
                                  VoucherHeader ON VoucherBody.VoucherID = VoucherHeader.VoucherID AND VoucherBody.VoucherType = VoucherHeader.VoucherType
            Where   (VoucherHeader.VoucherType = 'CRV' or VoucherHeader.VoucherType = 'CPV')
            AND (convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            UNION ALL
            SELECT     VoucherBody.Debit as Received,VoucherBody.Credit as Paid
            FROM         VoucherBody INNER JOIN
                                  VoucherHeader ON VoucherBody.VoucherID = VoucherHeader.VoucherID AND VoucherBody.VoucherType = VoucherHeader.VoucherType
            Where   VoucherHeader.VoucherType = 'JV' and VoucherBody.AccountNo='100000'
            AND (convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) < Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))))
            )OpeBal
            --Opening Ends
            UNION ALL
            Select PurchaseID,'PUR',VendorID,EntryDate,Narration,0,CashPaid from Purchase
            Where convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            UNION ALL
            Select PurReturnID,'PUR-R',VendorID,EntryDate,Narration,CashReceived,0 from PurReturn
            Where convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            Union All
Select IssueID,'CI',CustomerID,IssueDate,Remarks,SecurityDeposit,0 from CustomerIssue            
            Where convert(datetime,Convert(varchar,IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
UNION ALL
Select ReturnID,'CR',CustomerID,ReturnDate,Remarks,0,SecurityReturn from CustomerReturn            
            Where convert(datetime,Convert(varchar,ReturnDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
UNION ALL
Select RecoveryID,'REC',1,RecoveryDate,Remarks,TotalRecovery,0 from Recovery            
            Where convert(datetime,Convert(varchar,RecoveryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
Union ALL
            Select SaleID,'SAL',CustomerID,EntryDate,Narration,CashReceived,0 from Sale
            Where convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            Union ALL
            Select SaleReturnID,'SAL-R',CustomerID,EntryDate,Narration,0,CashPaid from SaleReturn
            Where convert(datetime,Convert(varchar,EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            Union All
            SELECT     BankDeposits.DepositID,CASE WHEN Isnull(BankDeposits.IsCheque, 0) = 0 THEN 'BDV' ELSE 'CDV' END AS InvType,BankDeposits.AccountNo, BankDeposits.DepositDate,  
                                              'Bank Deposit;' + BankDeposits.Narration, 0 AS Debit, BankDeposits.Amount AS Credit
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.AccountNo = AccountChart.AccountNo
                        Where BankDeposits.AccountNo =100000 AND  convert(datetime,Convert(varchar,BankDeposits.DepositDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))    
UNION ALL
SELECT     BankIssues.IssueID,'BCI' as InvType, BankIssues.BankAccountNo, BankIssues.IssueDate, BankIssues.Narration, BankIssueBody.Amount as Debit , 0 as Credit
FROM         BankIssues INNER JOIN
                      BankIssueBody ON BankIssues.IssueID = BankIssueBody.IssueID INNER JOIN
                      AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo
Where BankIssueBody.AccountNo =100000 AND  convert(datetime,Convert(varchar,BankIssues.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))    
						
            Union All
            SELECT     VoucherHeader.VoucherID AS InvoiceNo, VoucherHeader.VoucherType AS InvoiceType,VoucherBody.AccountNo,VoucherHeader.VoucherDate as InvoiceDate, VoucherBody.Remarks, 
                                  VoucherBody.Credit as Received,VoucherBody.Debit as Paid
            FROM         VoucherBody INNER JOIN
                                  VoucherHeader ON VoucherBody.VoucherID = VoucherHeader.VoucherID AND VoucherBody.VoucherType = VoucherHeader.VoucherType
            Where   (VoucherHeader.VoucherType = 'CRV' or VoucherHeader.VoucherType = 'CPV')
            And convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            UNION ALL
            SELECT     VoucherHeader.VoucherID AS InvoiceNo, VoucherHeader.VoucherType AS InvoiceType,VoucherBody.AccountNo,VoucherHeader.VoucherDate as InvoiceDate, VoucherBody.Remarks, 
                                  VoucherBody.Debit as Received,VoucherBody.Credit as Paid
            FROM         VoucherBody INNER JOIN
                                  VoucherHeader ON VoucherBody.VoucherID = VoucherHeader.VoucherID AND VoucherBody.VoucherType = VoucherHeader.VoucherType
            Where   VoucherHeader.VoucherType = 'JV' and VoucherBody.AccountNo='100000'
            And convert(datetime,Convert(varchar,VoucherHeader.VoucherDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1)))and Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1)))
            )AllData LEFT OUTER JOIN
                                  AccountChart ON AllData.AccountNo = AccountChart.AccountNo LEFT OUTER JOIN
                                  Parties ON AllData.AccountNo = Parties.PartyID
            Where AllData.Received > 0 or AllData.Paid > 0
)tmpTable
order by InvoiceDate
";

           DataSet ds = new DataSet();

           try
           {
               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vFromDate, vToDate));
               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getTrialBalance(string vToDate)
       {
           string query = @"Select Case When AccountType='ASSET' then (Case When Credit > 0 Then 'LIABILITY' Else 'ASSET' end) else AccountType end as AccountType,
AccountNo,AccountTitle,Debit,Credit 
from
(
SELECT   AccountChart.AccountType ,

 AccountChart.AccountNo, AccountChart.AccountTitle, 
 CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) 
                      + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + isnull(SUM(AllBal.Credit),0)) > 0 THEN (ISNULL(AccountChart.OpeningDebit, 0) 
                      + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + isnull(SUM(AllBal.Credit),0)) ELSE 0 END AS Debit,                                             
                      CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + Isnull(SUM(AllBal.Credit),0)) 
                      < 0 THEN ABS((ISNULL(AccountChart.OpeningDebit, 0) + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) 
                      + Isnull(SUM(AllBal.Credit),0))) ELSE 0 END AS Credit                      
                      
FROM         AccountChart LEFT OUTER JOIN
                          (SELECT     AccountID, SUM(Debit) AS Debit, SUM(Credit) AS Credit
                            FROM          (SELECT     Parties.AccountID, Purchase.EntryDate AS InvoiceDate, Purchase.CashPaid AS Debit, Purchase.GrossValue AS Credit
                                                    FROM          Purchase INNER JOIN
                                                                           Parties ON Purchase.VendorID = Parties.PartyID
                                                    UNION ALL
                                                    SELECT     '100000', Purchase.EntryDate AS InvoiceDate, 0 AS Debit, Purchase.CashPaid AS Credit
                                                    FROM          Purchase 
                                                    UNION ALL
                                                    SELECT     Parties_3.AccountID, PurReturn.EntryDate AS InvoiceDate, PurReturn.GrossValue AS Debit, PurReturn.CashReceived AS Credit
                                                    FROM         PurReturn INNER JOIN
                                                                          Parties AS Parties_3 ON PurReturn.VendorID = Parties_3.PartyID
                                                    UNION ALL
                                                    SELECT     '100000', PurReturn.EntryDate AS InvoiceDate, PurReturn.CashReceived AS Debit, 0 AS Credit
                                                    FROM          PurReturn 
                                                    UNION ALL
SELECT     Parties.AccountID ,CustomerIssue.IssueDate, 0  AS Debit, CustomerIssue.SecurityDeposit AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID							
UNION ALL
SELECT     '100000' ,CustomerIssue.IssueDate, CustomerIssue.SecurityDeposit  AS Debit, 0 AS Credit
							FROM         CustomerIssue INNER JOIN
												  Parties ON CustomerIssue.CustomerID = Parties.PartyID							
UNION ALL
SELECT     Parties.AccountID ,CustomerReturn.ReturnDate, CustomerReturn.SecurityReturn  AS Debit, 0 AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID
UNION ALL							
SELECT     '100000' AS PartyID,CustomerReturn.ReturnDate, 0  AS Debit, CustomerReturn.SecurityReturn AS Credit
							FROM         CustomerReturn INNER JOIN
												  Parties ON CustomerReturn.CustomerID = Parties.PartyID							
UNION ALL
SELECT     Parties.AccountID AS PartyID,Recovery.RecoveryDate, 0 as Debit, RecoveryBody.AmountRecoverd as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
UNION ALL
SELECT     '100000' AS PartyID,Recovery.RecoveryDate,RecoveryBody.AmountRecoverd as Debit, 0 as Credit
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID

UNION ALL
                                                    SELECT     Parties_2.AccountID, Sale.EntryDate AS InvoiceDate, Sale.GrossValue - Sale.SpecialDisc AS Debit, Sale.CashReceived AS Credit
                                                    FROM         Sale INNER JOIN
                                                                          Parties AS Parties_2 ON Sale.CustomerID = Parties_2.PartyID
                                                    UNION ALL
                                                    SELECT     '100000', Sale.EntryDate AS InvoiceDate, Sale.CashReceived AS Debit, 0 AS Credit
                                                    FROM         Sale  
                                                    UNION ALL
                                                    SELECT     Parties_1.AccountID, SaleReturn.EntryDate AS InvoiceDate, SaleReturn.CashPaid AS Debit, 
                                                                          SaleReturn.GrossValue - SaleReturn.SpecialDisc AS Credit
                                                    FROM         SaleReturn INNER JOIN
                                                                          Parties AS Parties_1 ON SaleReturn.CustomerID = Parties_1.PartyID
                                                    UNION ALL
                                                    SELECT     '100000', SaleReturn.EntryDate AS InvoiceDate, 0 AS Debit, SaleReturn.CashPaid AS Credit
                                                    FROM         SaleReturn
                                                    UNION ALL
                                                    SELECT     VoucherBody.AccountNo, VoucherHeader.VoucherDate, VoucherBody.Debit, VoucherBody.Credit
                                                    FROM         VoucherHeader INNER JOIN
                                                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                                                    UNION ALL
                                                    SELECT     '100000', VoucherHeader.VoucherDate, 0 as Debit, Sum(VoucherBody.Debit) as Credit
                                                    FROM         VoucherHeader INNER JOIN
                                                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                                                    WHERE  VoucherHeader.VoucherType='CPV'
                                                    Group By VoucherHeader.VoucherDate                                                    
                                                    UNION ALL
                                                    SELECT     '100000', VoucherHeader.VoucherDate, Sum(VoucherBody.Credit) as Debit, 0 as Credit
                                                    FROM         VoucherHeader INNER JOIN
                                                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                                                    WHERE  VoucherHeader.VoucherType='CRV'
                                                    Group By VoucherHeader.VoucherDate                                                    
                                                    UNION ALL
                                                    SELECT     BankDeposits.BankAccountNo, BankDeposits.DepositDate, BankDeposits.Amount AS Debit, 0 AS Credit
                                                    FROM         BankDeposits INNER JOIN
                                                                          AccountChart AS AccountChart_4 ON BankDeposits.BankAccountNo = AccountChart_4.AccountNo
                                                    UNION ALL
                                                    SELECT     BankDeposits_1.AccountNo, BankDeposits_1.DepositDate, 0 AS Debit, BankDeposits_1.Amount AS Credit
                                                    FROM         BankDeposits AS BankDeposits_1 INNER JOIN
                                                                          AccountChart AS AccountChart_3 ON BankDeposits_1.BankAccountNo = AccountChart_3.AccountNo
                                                    UNION ALL
                                                    SELECT     BankIssues.BankAccountNo, BankIssues.IssueDate, 0 AS Debit, BankIssues.Amount AS Credit
                                                    FROM         BankIssues INNER JOIN
                                                                          AccountChart AS AccountChart_2 ON BankIssues.BankAccountNo = AccountChart_2.AccountNo
                                                    UNION ALL
                                                    SELECT     '200000', BankIssues.IssueDate, 0 AS Debit, isnull(BankIssues.WHTAmount,0) AS Credit
                                                    FROM         BankIssues INNER JOIN
                                                                          AccountChart AS AccountChart_2 ON BankIssues.BankAccountNo = AccountChart_2.AccountNo
                                                    WHERE  isnull(BankIssues.WHTAmount,0) > 0                     
                                                    UNION ALL
                                                    SELECT     BankIssueBody.AccountNo, BankIssues_1.IssueDate, BankIssueBody.Amount AS Debit, 0 AS Credit
                                                    FROM         BankIssueBody INNER JOIN
                                                                          BankIssues AS BankIssues_1 ON BankIssueBody.IssueID = BankIssues_1.IssueID INNER JOIN
                                                                          AccountChart AS AccountChart_1 ON BankIssueBody.AccountNo = AccountChart_1.AccountNo

UNION ALL
SELECT '#SalExpenseAcc',GenerateDate,TotalSalary as Debit, 0 as Credit
FROM         Salary
UNION ALL
SELECT    Employees.AccountNo,Salary.GenerateDate, 0 as Debit, SalaryBody.NetSalary as Credit
FROM         SalaryBody INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID INNER JOIN
                      Salary ON SalaryBody.SalaryID = Salary.SalaryID
UNION ALL
SELECT     '#ProductWastage' as AccountNo,StockWastage.EntryDate, (StockWastageBody.Qty * StockWastageBody.Cost) as Debit,0 as Credit 
FROM         StockWastageBody INNER JOIN
                      StockWastage ON StockWastageBody.WasteID = StockWastage.WasteID
UNION ALL
SELECT    '#SampleIssuance' as AccountNo, StockSamples.IssueDate, (StockSampleBody.Qty * StockSampleBody.Cost) as Debit,0 as Credit 
FROM         StockSamples INNER JOIN
                      StockSampleBody ON StockSamples.SampleID = StockSampleBody.SampleID                      

) AS tmTable
                            WHERE      (Convert(smalldatetime,convert(varchar,InvoiceDate,1)) BETWEEN CONVERT(DATETIME, '1/1/2000 00:00:00', 102) AND CONVERT(DATETIME, '{0} 00:00:00', 102))
                            GROUP BY AccountID) AS AllBal ON AccountChart.AccountNo = AllBal.AccountID
Group By AccountChart.AccountType, AccountChart.AccountNo, AccountChart.AccountTitle,AccountChart.OpeningCredit,AccountChart.OpeningDebit

)temTable

";

           DataSet ds = new DataSet();

           try
           {
               Settings obj = new Settings();
               obj.connectionstring = connectionstring;

               query = query.Replace("#SalExpenseAcc", obj.GetSettingValue(Settings.ProSettings.SalaryExpAcc));
               query = query.Replace("#SampleIssuance", obj.GetSettingValue(Settings.ProSettings.SampleIssuance));
               query = query.Replace("#ProductWastage", obj.GetSettingValue(Settings.ProSettings.ProductWastage));

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vToDate));
               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }
       public DataTable getProfitAndLoss(string vFromDate, string vToDate,bool vOnlyExpense=false)
       {
           string query = @"SELECT   AccountChart.AccountType ,
 AccountChart.AccountNo, AccountChart.AccountTitle, 
 CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) 
                      + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + isnull(SUM(AllBal.Credit),0)) > 0 THEN (ISNULL(AccountChart.OpeningDebit, 0) 
                      + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + isnull(SUM(AllBal.Credit),0)) ELSE 0 END AS Debit, 
                      
                      
                      CASE WHEN (ISNULL(AccountChart.OpeningDebit, 0) + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) + Isnull(SUM(AllBal.Credit),0)) 
                      < 0 THEN ABS((ISNULL(AccountChart.OpeningDebit, 0) + Isnull(SUM(AllBal.Debit),0)) - (ISNULL(AccountChart.OpeningCredit, 0) 
                      + Isnull(SUM(AllBal.Credit),0))) ELSE 0 END AS Credit                      
                      
FROM         AccountChart LEFT OUTER JOIN
                          (SELECT     AccountID, SUM(Debit) AS Debit, SUM(Credit) AS Credit
                            FROM          (SELECT     Parties.AccountID, Purchase.EntryDate AS InvoiceDate, Purchase.CashPaid AS Debit, Purchase.GrossValue AS Credit
                                                    FROM          Purchase INNER JOIN
                                                                           Parties ON Purchase.VendorID = Parties.PartyID
                                                    UNION ALL
                                                    SELECT     Parties_3.AccountID, PurReturn.EntryDate AS InvoiceDate, PurReturn.GrossValue AS Debit, PurReturn.CashReceived AS Credit
                                                    FROM         PurReturn INNER JOIN
                                                                          Parties AS Parties_3 ON PurReturn.VendorID = Parties_3.PartyID
                                                    UNION ALL
                                                    SELECT     Parties_2.AccountID, Sale.EntryDate AS InvoiceDate, Sale.GrossValue - Sale.SpecialDisc AS Debit, Sale.CashReceived AS Credit
                                                    FROM         Sale INNER JOIN
                                                                          Parties AS Parties_2 ON Sale.CustomerID = Parties_2.PartyID
                                                    UNION ALL
                                                    SELECT     Parties_1.AccountID, SaleReturn.EntryDate AS InvoiceDate, SaleReturn.CashPaid AS Debit, 
                                                                          SaleReturn.GrossValue - SaleReturn.SpecialDisc AS Credit
                                                    FROM         SaleReturn INNER JOIN
                                                                          Parties AS Parties_1 ON SaleReturn.CustomerID = Parties_1.PartyID
                                                    UNION ALL
                                                    SELECT     VoucherBody.AccountNo, VoucherHeader.VoucherDate, VoucherBody.Debit, VoucherBody.Credit
                                                    FROM         VoucherHeader INNER JOIN
                                                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                                                    UNION ALL
                                                    SELECT     BankDeposits.BankAccountNo, BankDeposits.DepositDate, BankDeposits.Amount AS Debit, 0 AS Credit
                                                    FROM         BankDeposits INNER JOIN
                                                                          AccountChart AS AccountChart_4 ON BankDeposits.BankAccountNo = AccountChart_4.AccountNo
                                                    UNION ALL
                                                    SELECT     BankDeposits_1.AccountNo, BankDeposits_1.DepositDate, 0 AS Debit, BankDeposits_1.Amount AS Credit
                                                    FROM         BankDeposits AS BankDeposits_1 INNER JOIN
                                                                          AccountChart AS AccountChart_3 ON BankDeposits_1.BankAccountNo = AccountChart_3.AccountNo
                                                    UNION ALL
                                                    SELECT     BankIssues.BankAccountNo, BankIssues.IssueDate, 0 AS Debit, BankIssues.Amount AS Credit
                                                    FROM         BankIssues INNER JOIN
                                                                          AccountChart AS AccountChart_2 ON BankIssues.BankAccountNo = AccountChart_2.AccountNo
                                                    UNION ALL
                                                    SELECT     BankIssueBody.AccountNo, BankIssues_1.IssueDate, BankIssueBody.Amount AS Debit, 0 AS Credit
                                                    FROM         BankIssueBody INNER JOIN
                                                                          BankIssues AS BankIssues_1 ON BankIssueBody.IssueID = BankIssues_1.IssueID INNER JOIN
                                                                          AccountChart AS AccountChart_1 ON BankIssueBody.AccountNo = AccountChart_1.AccountNo
UNION ALL
SELECT     Parties.AccountID as AccountNo, CustomerIssue.IssueDate,0 as Debit, CustomerIssue.SecurityDeposit as Credit
FROM         CustomerIssue INNER JOIN
                      Parties ON CustomerIssue.CustomerID = Parties.PartyID
UNION ALL
SELECT     Parties.AccountID as AccountNo, CustomerReturn.ReturnDate, CustomerReturn.SecurityReturn as Debit, 0 as Credit
FROM         CustomerReturn INNER JOIN
                      Parties ON CustomerReturn.CustomerID = Parties.PartyID
UNION ALL
SELECT '#SalExpenseAcc',GenerateDate,TotalSalary as Debit, 0 as Credit
FROM         Salary
UNION ALL
SELECT    Employees.AccountNo,Salary.GenerateDate, 0 as Debit, SalaryBody.NetSalary as Credit
FROM         SalaryBody INNER JOIN
                      Employees ON SalaryBody.EmployeeID = Employees.EmployeeID INNER JOIN
                      Salary ON SalaryBody.SalaryID = Salary.SalaryID
UNION ALL 
SELECT     '#ProductWastage' as AccountNo,StockWastage.EntryDate, (StockWastageBody.Qty * StockWastageBody.Cost) as Debit,0 as Credit 
FROM         StockWastageBody INNER JOIN
                      StockWastage ON StockWastageBody.WasteID = StockWastage.WasteID
UNION ALL
SELECT    '#SampleIssuance' as AccountNo, StockSamples.IssueDate, (StockSampleBody.Qty * StockSampleBody.Cost) as Debit,0 as Credit 
FROM         StockSamples INNER JOIN
                      StockSampleBody ON StockSamples.SampleID = StockSampleBody.SampleID                      
) AS tmTable
                            WHERE      (Convert(smalldatetime,convert(varchar,InvoiceDate,102)) BETWEEN CONVERT(DATETIME, '{0} 00:00:00', 102) AND CONVERT(DATETIME, '{1} 00:00:00', 102))
                            GROUP BY AccountID
) AS AllBal ON AccountChart.AccountNo = AllBal.AccountID
Where 1=1 #ShowType 
Group By AccountChart.AccountType, AccountChart.AccountNo, AccountChart.AccountTitle,AccountChart.OpeningCredit,AccountChart.OpeningDebit

";

           DataSet ds = new DataSet();

           try
           {
               Settings obj = new Settings();
               obj.connectionstring = connectionstring;

               query = query.Replace("#SalExpenseAcc", obj.GetSettingValue(Settings.ProSettings.SalaryExpAcc));
               query = query.Replace("#SampleIssuance", obj.GetSettingValue(Settings.ProSettings.SampleIssuance));
               query = query.Replace("#ProductWastage", obj.GetSettingValue(Settings.ProSettings.ProductWastage));

               if (!vOnlyExpense)
               {
                   query = query.Replace("#ShowType", " AND (AccountType='EXPENSE' or AccountType='REVENUE')");
               }
               else {
                   query = query.Replace("#ShowType", " AND (AccountType='EXPENSE')");
               }


               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query,vFromDate, vToDate));
               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getStockStatement(string vFromDate, string vToDate,string vWhere)
       {
           string query = @"SELECT     Products.ProductID, Products.ProductName, UnitsInfo.UnitTitle, Products.OpeningStock, Products.OpeningStockValue, ISNULL(SUM(Purchases.Qty), 0) 
                      AS PurchaseQty, ISNULL(SUM(Purchases.TotalValue), 0) AS PurchaseValue, ISNULL(SUM(PurReturns.Qty), 0) AS PurRetQty, ISNULL(SUM(PurReturns.TotalValue), 0) 
                      AS PurRetValue, ISNULL(SUM(Sales.Qty), 0) AS SaleQty, ISNULL(SUM(Sales.TotalValue), 0) AS SaleValue, ISNULL(SUM(SaleRet.Qty), 0) AS SaleRetQty, 
                      ISNULL(SUM(SaleRet.TotalValue), 0) AS SaleRetValue, ISNULL(SUM(Consumption.Qty), 0) AS ConsumQty, ISNULL(SUM(Consumption.TotalValue), 0) AS ConsumValue, 
                      ISNULL(SUM(Production.Qty), 0) AS ProdQty, ISNULL(SUM(Production.TotalValue), 0) AS ProdValue, SUM(ISNULL(Wastage.Qty, 0)) AS WasteQty, 
                      SUM(ISNULL(Wastage.TotalValue, 0)) AS WasteValue, SUM(ISNULL(Issued.Qty, 0)) AS IssueQty, SUM(ISNULL(Issued.TotalValue, 0)) AS IssueValue, 
                      SUM(ISNULL(Returned.Qty, 0)) AS RetQty, SUM(ISNULL(Returned.TotalValue, 0)) AS RetValue,
SUM(ISNULL(Samples.Qty, 0)) AS SampleQty, SUM(ISNULL(Samples.TotalValue, 0)) AS SampleValue

FROM         Products INNER JOIN
                      UnitsInfo ON Products.UnitID = UnitsInfo.UnitID LEFT OUTER JOIN


(SELECT     CustomerIssueBody.ProductID, SUM(CustomerIssueBody.Qty) AS Qty, SUM(CustomerIssueBody.Qty * CustomerIssueBody.Cost) AS TotalValue
                            FROM          CustomerIssueBody INNER JOIN
                                                   CustomerIssue ON CustomerIssueBody.IssueID = CustomerIssue.IssueID
Where convert(datetime,Convert(varchar,CustomerIssue.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            GROUP BY CustomerIssueBody.ProductID) AS Issued ON Products.ProductID = Issued.ProductID LEFT OUTER JOIN
                          (SELECT     StockWastageBody.ProductID, SUM(StockWastageBody.Qty) AS Qty, SUM(StockWastageBody.Qty * StockWastageBody.Cost) AS TotalValue
                            FROM          StockWastageBody INNER JOIN
                                                   StockWastage ON StockWastageBody.WasteID = StockWastage.WasteID
Where convert(datetime,Convert(varchar,StockWastage.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            GROUP BY StockWastageBody.ProductID) AS Wastage ON Products.ProductID = Wastage.ProductID LEFT OUTER JOIN
                          (SELECT     CustomerReturnBody.ProductID, SUM(CustomerReturnBody.Qty) AS Qty, 0 AS TotalValue
                            FROM          CustomerReturnBody INNER JOIN
                                                   CustomerReturn ON CustomerReturnBody.ReturnID = CustomerReturn.ReturnID
Where convert(datetime,Convert(varchar,CustomerReturn.ReturnDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
                            GROUP BY CustomerReturnBody.ProductID) AS Returned ON Products.ProductID = Returned.ProductID LEFT OUTER JOIN
(SELECT     StockSampleBody.ProductID, sum(StockSampleBody.Qty) as Qty, sum(StockSampleBody.Qty * StockSampleBody.Cost) AS TotalValue
                            FROM          StockSampleBody INNER JOIN
                                                   StockSamples ON StockSampleBody.SampleID = StockSamples.SampleID
Where convert(datetime,Convert(varchar,StockSamples.IssueDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by StockSampleBody.ProductID
) AS Samples ON Products.ProductID = Samples.ProductID LEFT OUTER JOIN


                          (SELECT     ProductionBody.ProductID, sum(ProductionBody.Qty) as Qty, sum(ProductionBody.Qty * ProductionBody.Cost) AS TotalValue
                            FROM          ProductionBody INNER JOIN
                                                   Production AS Production_1 ON ProductionBody.ProductionID = Production_1.ProductionID
Where convert(datetime,Convert(varchar,Production_1.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by ProductionBody.ProductID
) AS Production ON 
                      Products.ProductID = Production.ProductID LEFT OUTER JOIN
                          (SELECT     Consumption_1.ProductID, sum(Consumption_1.Qty) as Qty, sum(Consumption_1.Qty * Consumption_1.Cost) AS TotalValue
                            FROM          Consumption AS Consumption_1 INNER JOIN
                                                   Production AS Production_2 ON Consumption_1.ProductionID = Production_2.ProductionID
Where convert(datetime,Convert(varchar,Production_2.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by Consumption_1.ProductID
) AS Consumption ON 
                      Products.ProductID = Consumption.ProductID LEFT OUTER JOIN
                          (SELECT     SaleRetBody.ProductID, sum(SaleRetBody.Qty) as Qty, sum(SaleRetBody.Qty * SaleBody_1.Cost) AS TotalValue
                            FROM          SaleReturn INNER JOIN
                                                   SaleRetBody ON SaleReturn.SaleReturnID = SaleRetBody.SaleReturnID INNER JOIN
                                                   SaleBody AS SaleBody_1 ON SaleReturn.SaleID = SaleBody_1.SaleID AND SaleRetBody.ProductID = SaleBody_1.ProductID
Where convert(datetime,Convert(varchar,SaleReturn.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by SaleRetBody.ProductID
) AS SaleRet ON 
                      Products.ProductID = SaleRet.ProductID LEFT OUTER JOIN
                          (SELECT     SaleBody.ProductID, sum(SaleBody.Qty) as Qty, sum(SaleBody.Qty * SaleBody.Cost) AS TotalValue
                            FROM          SaleBody INNER JOIN
                                                   Sale ON SaleBody.SaleID = Sale.SaleID
Where convert(datetime,Convert(varchar,Sale.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by SaleBody.ProductID
) AS Sales ON Products.ProductID = Sales.ProductID LEFT OUTER JOIN
                          (SELECT     PurReturnBody.ProductID, sum(PurReturnBody.Qty) as Qty, sum(PurReturnBody.TotalValue) as TotalValue
                            FROM          PurReturnBody INNER JOIN
                                                   PurReturn ON PurReturnBody.PurReturnID = PurReturn.PurReturnID
Where convert(datetime,Convert(varchar,PurReturn.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by PurReturnBody.ProductID
) AS PurReturns ON Products.ProductID = PurReturns.ProductID LEFT OUTER JOIN
                          (SELECT     PurchaseBody.ProductID, sum(PurchaseBody.Qty) as Qty, sum(PurchaseBody.TotalValue) as TotalValue
                            FROM          PurchaseBody INNER JOIN
                                                   Purchase ON PurchaseBody.PurchaseID = Purchase.PurchaseID
Where convert(datetime,Convert(varchar,Purchase.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{2} 00:00:00',102),1)))
group by PurchaseBody.ProductID
) AS Purchases ON Products.ProductID = Purchases.ProductID
Where 1=1 {0}
GROUP BY Products.ProductID, Products.ProductName, UnitsInfo.UnitTitle, Products.OpeningStock, Products.OpeningStockValue

";

           DataSet ds = new DataSet();

           try
           {
               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query,vWhere, vFromDate, vToDate ));
               return ds.Tables[0];
           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getPartyProductWiseSale(string vCustomerID, int vSalesmanID, string vFromDate,string vToDate)
       {
           string query = @"
SELECT     SaleData.CustomerID,Parties.PartyName, SaleData.ProductID,Products.ProductName,
 Sum(SaleData.SaleQty) as SaleQty,SUM(SaleData.SaleValue) as SaleValue,
 SUM(SaleData.RetQty) as RetQty, Sum(SaleData.RetValue) as RetValue
  
FROM         (SELECT     Sale.CustomerID, SaleBody.ProductID, SUM(SaleBody.Qty) AS SaleQty, SUM(SaleBody.TotalValue) AS SaleValue, 0 AS RetQty, 0 AS RetValue
                       FROM          Sale INNER JOIN
                                              SaleBody ON Sale.SaleID = SaleBody.SaleID
Where  1=1 AND convert(datetime,Convert(varchar,Sale.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) #SalesmanA  #CustomerA
                       GROUP BY Sale.CustomerID, SaleBody.ProductID
                       UNION ALL
                       SELECT     SaleReturn.CustomerID, SaleRetBody.ProductID, 0 AS SaleQty, 0 AS SaleValue, SUM(SaleRetBody.Qty) AS RetQty, SUM(SaleRetBody.TotalValue) 
                                             AS RetValue
                       FROM         SaleReturn INNER JOIN
                                             SaleRetBody ON SaleReturn.SaleReturnID = SaleRetBody.SaleReturnID
Where  1=1 AND convert(datetime,Convert(varchar,SaleReturn.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) #SalesmanB #CustomerB 
                       GROUP BY SaleReturn.CustomerID, SaleRetBody.ProductID) AS SaleData INNER JOIN
                      Products ON SaleData.ProductID = Products.ProductID INNER JOIN
                      Parties ON SaleData.CustomerID = Parties.PartyID
Group by SaleData.CustomerID,Parties.PartyName, SaleData.ProductID,Products.ProductName                      
order by CustomerID,ProductID
                ";


           if (!string.IsNullOrEmpty(vCustomerID))
           {
               query = query.Replace("#CustomerA", " AND Sale.CustomerID=" + vCustomerID);
               query = query.Replace("#CustomerB", " AND SaleReturn.CustomerID=" + vCustomerID);
           }
           else
           {
               query = query.Replace("#CustomerA", "");
               query = query.Replace("#CustomerB", "");
           }

           if (vSalesmanID > 0)
           {
               query = query.Replace("#SalesmanA", " AND Sale.SalesmanID=" + vSalesmanID);
               query = query.Replace("#SalesmanB", " AND SaleReturn.SaleID in (Select SaleID From Sale Where SalesmanID=" + vSalesmanID + ")");
           }
           else
           {
               query = query.Replace("#SalesmanA", "");
               query = query.Replace("#SalesmanB", "");
           }

           DataSet ds = new DataSet();

           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vFromDate,vToDate));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getPartySaleSummary(string vCustomerID, int vSalesmanID, string vFromDate, string vToDate)
       {
           string query = @"SELECT     Sale.SaleID, Sale.EntryDate, Sale.CustomerID, Parties.PartyName,dbo.FunGetSaleNarration(Sale.SaleID) as Description, Sale.GrossValue, Sale.SpecialDisc, Sale.Narration, Sale.CashReceived
FROM         Sale INNER JOIN
                      Parties ON Sale.CustomerID = Parties.PartyID
Where 1=1 AND convert(datetime,Convert(varchar,Sale.EntryDate,1)) Between Convert(Datetime,(convert(varchar,convert(Datetime,'{0} 00:00:00',102),1))) AND Convert(Datetime,(convert(varchar,convert(Datetime,'{1} 00:00:00',102),1))) #Customer #Salesman
Order By Sale.EntryDate
                ";


           if (!string.IsNullOrEmpty(vCustomerID))
           {
               query = query.Replace("#Customer", " AND Sale.CustomerID=" + vCustomerID);               
           }
           else
           {
               query = query.Replace("#Customer", "");               
           }

           if (vSalesmanID > 0)
           {
               query = query.Replace("#Salesman", " AND Sale.SalesmanID=" + vSalesmanID);               
           }
           else
           {
               query = query.Replace("#Salesman", "");               
           }

           DataSet ds = new DataSet();

           try
           {

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vFromDate, vToDate));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getSalesmanRecovery(string vWhere)
       {
           string query = @"SELECT     Recovery.RecoveryID, Recovery.RecoveryDate, Recovery.SalesmanID, Employees.EmployeeName, RecoveryBody.CustomerID, Parties.PartyName, 
                      RecoveryBody.AmountRecoverd, Recovery.Remarks
FROM         Recovery INNER JOIN
                      RecoveryBody ON Recovery.RecoveryID = RecoveryBody.RecoveryID INNER JOIN
                      Employees ON Recovery.SalesmanID = Employees.EmployeeID INNER JOIN
                      Parties ON RecoveryBody.CustomerID = Parties.PartyID
Where 1=1 {0}
ORDER BY Recovery.RecoveryDate
                      
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

       public DataTable getPartiesAging(string vWhere, Int64 vSalesmanID)
       {
           string query = @"
SELECT     LastPurchase.CustomerID, LastPurchase.LastPurchaseDate,Parties.PartyName, DATEDIFF(day, LastPurchase.LastPurchaseDate, GETDATE()) AS DaysDiff 
FROM         (SELECT     CustomerID, MAX(EntryDate) AS LastPurchaseDate
                       FROM          Sale
                        Where 1=1 #Salesman
                       GROUP BY CustomerID) AS LastPurchase INNER JOIN
                      Parties ON LastPurchase.CustomerID = Parties.PartyID 
                Where 1=1 {0}";

           DataSet ds = new DataSet();

           try
           {

               if (vSalesmanID > 0)
               {
                   query = query.Replace("#Salesman", " AND SalesmanID=" + vSalesmanID);
               }
               else query = query.Replace("#Salesman", string.Empty);

               ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vWhere));
               return ds.Tables[0];

           }
           catch (Exception exc)
           {

               throw exc;
           }

       }

       public DataTable getPartiesAgingRec(string vWhere, Int64 vSalesmanID)
       {
           string query = @"
SELECT     LastPurchase.CustomerID, LastPurchase.LastPurchaseDate, Parties.PartyName, DATEDIFF(day, LastPurchase.LastPurchaseDate, GETDATE()) AS DaysDiff, 
                      Recovery.LastRecoveryDate,DATEDIFF(day, Recovery.LastRecoveryDate, GETDATE()) as DaysDiffRec
FROM         (SELECT     CustomerID, MAX(EntryDate) AS LastPurchaseDate
                       FROM          Sale
                       WHERE      (1 = 1) #Salesman
                       GROUP BY CustomerID) AS LastPurchase INNER JOIN
                      Parties ON LastPurchase.CustomerID = Parties.PartyID LEFT OUTER JOIN
                          (SELECT     RecoveryBody.CustomerID, MAX(Recovery_1.RecoveryDate) AS LastRecoveryDate
                            FROM          Recovery AS Recovery_1 INNER JOIN
                                                   RecoveryBody ON Recovery_1.RecoveryID = RecoveryBody.RecoveryID
                            WHERE (1=1) #Salesman
                            GROUP BY RecoveryBody.CustomerID) AS Recovery ON Parties.PartyID = Recovery.CustomerID
WHERE     (1 = 1)  {0}";

           DataSet ds = new DataSet();

           try
           {

               if (vSalesmanID > 0)
               {
                   query = query.Replace("#Salesman", " AND SalesmanID=" + vSalesmanID);
               }
               else query = query.Replace("#Salesman", string.Empty);

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
