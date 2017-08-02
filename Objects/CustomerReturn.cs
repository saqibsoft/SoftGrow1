using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class CustomerReturn
    {
        private Int64 _ReturnID;
        private DateTime _ReturnDate;
        private Int64 _IssueID;
        private Int64 _CustomerID;
        private decimal _SecurityReturn;
        private int _UserID;
        private string _Remarks;

        public Int64 ReturnID { get { return _ReturnID; } set { _ReturnID = value; } }
        public DateTime ReturnDate { get { return _ReturnDate; } set { _ReturnDate = value; } }
        public Int64 IssueID { get { return _IssueID; } set { _IssueID = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public decimal SecurityReturn { get { return _SecurityReturn; } set { _SecurityReturn = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
