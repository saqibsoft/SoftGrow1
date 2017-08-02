using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class CustomerIssue
    {
        private Int64 _IssueID;
        private DateTime _IssueDate;
        private Int64 _CustomerID;
        private decimal _SecurityDeposit;
        private int _UserID;
        private string _Remarks;

        public Int64 IssueID { get { return _IssueID; } set { _IssueID = value; } }
        public DateTime IssueDate { get { return _IssueDate; } set { _IssueDate = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public decimal SecurityDeposit { get { return _SecurityDeposit; } set { _SecurityDeposit = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
