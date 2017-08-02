using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class BankDeposits
    {
        private Int64 _DepositID;
        private DateTime _DepositDate;
        private bool _IsCheque;
        private string _ChequeNo;
        private DateTime _ChequeDate;
        private string _SlipNo;
        private string _BankAccountNo;
        private string _AccountNo;
        private decimal _Amount;
        private string _DepositedBy;
        private string _Narration;
        private Int32 _UserID;
        private DateTime _EntryDate;
        private bool _IsPosted;


        public Int64 DepositID { get { return _DepositID; } set { _DepositID = value; } }
        public DateTime DepositDate { get { return _DepositDate; } set { _DepositDate = value; } }
        public bool IsCheque { get { return _IsCheque; } set { _IsCheque = value; } }
        public string ChequeNo { get { return _ChequeNo; } set { _ChequeNo = value; } }
        public DateTime ChequeDate { get { return _ChequeDate; } set { _ChequeDate = value; } }
        public string SlipNo { get { return _SlipNo; } set { _SlipNo = value; } }
        public string BankAccountNo { get { return _BankAccountNo; } set { _BankAccountNo = value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
        public decimal Amount { get { return _Amount; } set { _Amount = value; } }
        public string DepositedBy { get { return _DepositedBy; } set { _DepositedBy = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public Int32 UserID { get { return _UserID; } set { _UserID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public bool IsPosted { get { return _IsPosted; } set { _IsPosted = value; } }
    }
}
