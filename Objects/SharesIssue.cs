using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class SharesIssue
    {
        private Int64 _IssueID;
        private DateTime _IssueDate;
        private Int64 _MemberID;
        private int _SchemeID;
        private Int32 _NoOfShares;
        private decimal _PerShareValue;
        private string _ModeofPayment;
        private string _ChequeNo;
        private string _Remarks;
        private int _UserID;

        public Int64 IssueID { get { return _IssueID; } set { _IssueID = value; } }
        public DateTime IssueDate { get { return _IssueDate; } set { _IssueDate = value; } }
        public Int64 MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public int SchemeID { get { return _SchemeID; } set { _SchemeID = value; } }
        public Int32 NoOfShares { get { return _NoOfShares; } set { _NoOfShares = value; } }
        public decimal PerShareValue { get { return _PerShareValue; } set { _PerShareValue = value; } }
        public string ModeofPayment { get { return _ModeofPayment; } set { _ModeofPayment = value; } }
        public string ChequeNo { get { return _ChequeNo; } set { _ChequeNo = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }


    }
}
