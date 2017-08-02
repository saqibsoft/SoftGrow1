using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class BankAccounts
    {

        private Int32 _BankAccountID;
        private string _AccountTitle;
        private string _BankName;
        private string _BranchCode;
        private string _BranchName;
        private string _AccountID;

        public Int32 BankAccountID { get { return _BankAccountID; } set { _BankAccountID = value; } }
        public string AccountTitle { get { return _AccountTitle; } set { _AccountTitle = value; } }
        public string BankName { get { return _BankName; } set { _BankName = value; } }
        public string BranchCode { get { return _BranchCode; } set { _BranchCode = value; } }
        public string BranchName { get { return _BranchName; } set { _BranchName = value; } }
        public string AccountID { get { return _AccountID; } set { _AccountID = value; } }
    }
}
