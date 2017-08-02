using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
	public class Sale
	{
		public string connectionstring;
		public void InsertRecord(Objects.Sale obj)
		{
			try
			{

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_SaleInsert";

				cmd.Parameters.AddWithValue("@SaleID", obj.SaleID);
				cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
				cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
				if (obj.SalesmanID > 0)
				{
					cmd.Parameters.AddWithValue("@SalesmanID", obj.SalesmanID);
				}
				else cmd.Parameters.AddWithValue("@SalesmanID", null);

				cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
				cmd.Parameters.AddWithValue("@SpecialDisc", obj.SpecialDisc);
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
		public void InsertRecordBody(Objects.SaleBody obj)
		{
			try
			{

				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_SaleBodyInsert";
				
				cmd.Parameters.AddWithValue("@SaleID", obj.SaleID);
				cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
				cmd.Parameters.AddWithValue("@Qty", obj.Qty);
				cmd.Parameters.AddWithValue("@Price", obj.Price);
				cmd.Parameters.AddWithValue("@Discount", obj.Discount);
				cmd.Parameters.AddWithValue("@TotalValue", obj.TotalValue);
				cmd.Parameters.AddWithValue("@Cost", obj.Cost);
				cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);

				new Database(connectionstring).ExecuteNonQueryOnly(cmd);
			}
			catch (Exception exc)
			{

				throw exc;
			}

		}
		public void UpdateRecord(Objects.Sale obj)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_SaleUpdate";

				cmd.Parameters.AddWithValue("@SaleID", obj.SaleID);
				cmd.Parameters.AddWithValue("@EntryDate", obj.EntryDate);
				cmd.Parameters.AddWithValue("@CustomerID", obj.CustomerID);
				if (obj.SalesmanID > 0)
				{
					cmd.Parameters.AddWithValue("@SalesmanID", obj.SalesmanID);
				}
				else cmd.Parameters.AddWithValue("@SalesmanID", null);
				cmd.Parameters.AddWithValue("@GrossValue", obj.GrossValue);
				cmd.Parameters.AddWithValue("@SpecialDisc", obj.SpecialDisc);
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
				cmd.CommandText = "SP_SaleDelete";

				cmd.Parameters.AddWithValue("@SaleID", vID);
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
				string vSql = "Delete From SaleBody Where SaleID={0} ";

				new Database(connectionstring).ExecuteNonQueryOnly(string.Format(vSql,vID));

			}
			catch (Exception exc)
			{

				throw exc;
			}
		}
		public DataTable getRecord(string vWhere)
		{
			string query = @"SELECT     Sale.SaleID, Sale.EntryDate, Sale.CustomerID, Parties.PartyName, Sale.SalesmanID, Employees.EmployeeName, Sale.GrossValue, Sale.SpecialDisc, Sale.Narration, 
					  Sale.UserID, Sale.CashReceived, SaleBody.ProductID, Products.ProductName, SaleBody.Qty,SaleBody.Cost, SaleBody.Price, SaleBody.Discount, SaleBody.TotalValue, SaleBody.Remarks,
					  Products.UnitID, UnitsInfo.UnitTitle, 0 as Units
				FROM         Sale INNER JOIN
					  SaleBody ON Sale.SaleID = SaleBody.SaleID INNER JOIN
					  Parties ON Sale.CustomerID = Parties.PartyID INNER JOIN
					  Products ON SaleBody.ProductID = Products.ProductID LEFT OUTER JOIN
					  Employees ON Sale.SalesmanID = Employees.EmployeeID LEFT OUTER JOIN
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
		public int getNextNo()
		{
			int vNextID = 1;

			string vSql = "Select COALESCE(MAX(Isnull(SaleID,0)),0) + 1  as SaleID From Sale ";


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
		public decimal getBalance(string vAccountNo)
		{
			decimal vAmount = 0;

			string vSql = @"Select  ABS(Debit - Credit) as Amount from 
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
																		  AccountChart AS AccountChart_1 ON BankIssueBody.AccountNo = AccountChart_1.AccountNo) AS tmTable
							WHERE  (convert(datetime,Convert(varchar,InvoiceDate,1)) BETWEEN CONVERT(DATETIME, '1/1/2000 00:00:00', 102) AND CONVERT(DATETIME, GETDATE(), 102))
							GROUP BY AccountID) AS AllBal ON AccountChart.AccountNo = AllBal.AccountID
where AccountChart.IsParty=1                             
Group By AccountChart.AccountType, AccountChart.AccountNo, AccountChart.AccountTitle,AccountChart.OpeningCredit,AccountChart.OpeningDebit
)tmpTable
Where AccountNo='{0}'
 ";


			try
			{
				DataSet ds = new Database(connectionstring).ExecuteForDataSet(string.Format(vSql, vAccountNo));
				if(ds.Tables[0].Rows.Count > 0)
				vAmount = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

			}
			catch (Exception exc)
			{

				throw exc;
			}
			return vAmount;
		}

	}
}
