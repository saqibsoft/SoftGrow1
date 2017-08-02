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
  public  class Searches
    {

      public string connectionstring;
      
      public DataTable getVouchers(string vWhere)
      {
          string query = @" SELECT     VoucherHeader.VoucherID AS VoucherNo, VoucherHeader.VoucherType, VoucherHeader.VoucherDate, SUM(VoucherBody.Debit) AS Debit, SUM(VoucherBody.Credit) 
                      AS Credit
                    FROM         VoucherHeader INNER JOIN
                                          VoucherBody ON VoucherHeader.VoucherID = VoucherBody.VoucherID AND VoucherHeader.VoucherType = VoucherBody.VoucherType
                     Where 1 = 1 {0}
                    Group by   VoucherHeader.VoucherID , VoucherHeader.VoucherType, VoucherHeader.VoucherDate 
Order By  VoucherHeader.VoucherDate DESC
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

      public DataTable getDeposits(string vWhere)
      {
          string query = @" SELECT     BankDeposits.DepositID, BankDeposits.DepositDate, BankDeposits.BankAccountNo, AccountChart.AccountTitle as FromAccount, BankDeposits.AccountNo, 
                      AccountChart_1.AccountTitle AS ToAccount, BankDeposits.Amount
                        FROM         BankDeposits INNER JOIN
                                              AccountChart ON BankDeposits.BankAccountNo = AccountChart.AccountNo INNER JOIN
                                              AccountChart AS AccountChart_1 ON BankDeposits.AccountNo = AccountChart_1.AccountNo
                        WHERE     1=1 {0} Order By BankDeposits.DepositDate DESC";
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

      public DataTable getCheqIssues(string vWhere)
      {
          string query = @" SELECT     BankIssues.IssueID, BankIssues.IssueDate, BankIssues.BankAccountNo, BankIssues.Amount AS TotalAmount, AccountChart.AccountTitle AS BankAccountName, 
                      BankIssues.ChequeNo
                        FROM         BankIssues INNER JOIN
                                              AccountChart ON BankIssues.BankAccountNo = AccountChart.AccountNo 
                                              
                        WHERE     (1 = 1)
                         {0} Order By BankIssues.IssueDate DESC";
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

      public DataTable getParties(string vWhere, bool vWithAccounts = false, bool visJV = false)
      {
          string query = @" Select PartyID , PartyName,AccountID  from Parties Where 1 = 1 {0}";

          if (vWithAccounts == true)
          {
              query += "UNION ALL select convert(bigint,AccountNo) as AccountNo,AccountTitle from AccountChart";

              if (visJV == false) query += " Where AccountNo <> '100001'";
          }

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

      public DataTable getPurchases(string vWhere)
      {
          string query = @" SELECT     Purchase.PurchaseID, Purchase.EntryDate, Parties.PartyName
                        FROM         Purchase INNER JOIN
                      Parties ON Purchase.VendorID = Parties.PartyID Where 1 = 1 {0} Order By Purchase.EntryDate DESC";
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

      public DataTable getPurReturns(string vWhere)
      {
          string query = @" SELECT     PurReturn.PurReturnID as PurchaseID, PurReturn.EntryDate, Parties.PartyName
                        FROM         PurReturn INNER JOIN
                      Parties ON PurReturn.VendorID = Parties.PartyID Where 1 = 1 {0} Order By PurReturn.EntryDate DESC";
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

      public DataTable getSale(string vWhere)
      {
          string query = @" SELECT     Sale.SaleID, Sale.EntryDate, Parties.PartyName
                        FROM         Sale INNER JOIN
                      Parties ON Sale.CustomerID = Parties.PartyID Where 1 = 1 {0} Order By  Sale.EntryDate DESC";
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

      public DataTable getSharesIssue(string vWhere)
      {
          string query = @" SELECT     SharesIssue.IssueID, SharesIssue.IssueDate, SharesIssue.MemberID, ShareMembers.MemberName, SharesIssue.NoOfShares
FROM         ShareMembers INNER JOIN
                      SharesIssue ON ShareMembers.MemberID = SharesIssue.MemberID Where 1 = 1 {0}";
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

      public DataTable getSaleRet(string vWhere)
      {
          string query = @" SELECT     SaleReturn.SaleReturnID as SaleID, SaleReturn.EntryDate, Parties.PartyName
                        FROM         SaleReturn INNER JOIN
                      Parties ON SaleReturn.CustomerID = Parties.PartyID Where 1 = 1 {0}  Order By  SaleReturn.EntryDate DESC";
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

      public DataTable getEmployees(string vWhere)
      {
          string query = @" Select EmployeeID,EmployeeName from Employees Where 1 = 1 {0}";

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

      public DataTable getProducts(string vWhere)
      {
          string query = @"SELECT     Products.ProductID, Products.ProductName,Products.SalePrice,Products.CostPrice as PurchasePrice, Products.UnitID, 0 as Units, UnitsInfo.UnitTitle
                            FROM         Products LEFT OUTER JOIN
                                UnitsInfo ON Products.UnitID = UnitsInfo.UnitID Where 1 = 1 {0}";
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

      public DataTable getProduction(string vWhere)
      {
          string query = @" SELECT     ProductionID, EntryDate, Narration
                            FROM         Production Where 1 = 1 {0} Order By EntryDate DESC";

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
