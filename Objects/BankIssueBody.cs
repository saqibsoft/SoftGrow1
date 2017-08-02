using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class BankIssueBody
    {
        private Int64 _SerialNo;
        private Int64 _IssueID;
        private string _AccountNo;
        private decimal _Amount;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 IssueID { get { return _IssueID; } set { _IssueID = value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
        public decimal Amount { get { return _Amount; } set { _Amount = value; } }
    }
}
