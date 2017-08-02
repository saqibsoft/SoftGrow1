using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class BankIssues
    {
        private Int64 _IssueID;
        private DateTime _IssueDate;
        private string _ChequeNo;
        private DateTime _ChequeDate;
        private string _BankAccountNo;
        private decimal _Amount;
        private string _ReceivedBy;
        private bool _IsLost;
        private string _Narration;
        private int _UserID;
        private DateTime _EntryDate;
        private bool _IsPosted;
        private string _WHTAccountNo;
        private decimal _WHTAmount;        

        public Int64 IssueID { get { return _IssueID; } set { _IssueID = value; } }
        public DateTime IssueDate { get { return _IssueDate; } set { _IssueDate = value; } }
        public string ChequeNo { get { return _ChequeNo; } set { _ChequeNo = value; } }
        public DateTime ChequeDate { get { return _ChequeDate; } set { _ChequeDate = value; } }
        public string BankAccountNo { get { return _BankAccountNo; } set { _BankAccountNo = value; } }
        public decimal Amount { get { return _Amount; } set { _Amount = value; } }
        public string ReceivedBy { get { return _ReceivedBy; } set { _ReceivedBy = value; } }
        public bool IsLost { get { return _IsLost; } set { _IsLost = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public bool IsPosted { get { return _IsPosted; } set { _IsPosted = value; } }
        public string WHTAccountNo { get { return _WHTAccountNo; } set { _WHTAccountNo = value; } }
        public decimal WHTAmount { get { return _WHTAmount; } set { _WHTAmount = value; } }        
    }
}
