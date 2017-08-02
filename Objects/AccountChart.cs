using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public  class AccountChart
    {
        private string _AccountNo;
        private string _AccountTitle;
        private string _AccountType;
        private string _AccountSubType;
        private decimal _OpeningDebit;
        private decimal _OpeningCredit;
        private bool _IsParty;
        private bool _IsBank;
        private bool _IsEditable;


        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
        public string AccountTitle { get { return _AccountTitle; } set { _AccountTitle = value; } }
        public string AccountType { get { return _AccountType; } set { _AccountType = value; } }
        public string AccountSubType { get { return _AccountSubType; } set { _AccountSubType = value; } }
        public decimal OpeningDebit { get { return _OpeningDebit; } set { _OpeningDebit = value; } }
        public decimal OpeningCredit { get { return _OpeningCredit; } set { _OpeningCredit = value; } }
        public bool IsParty { get { return _IsParty; } set { _IsParty = value; } }
        public bool IsBank { get { return _IsBank; } set { _IsBank = value; } }
        public bool IsEditable { get { return _IsEditable; } set { _IsEditable = value; } }
    }
}
