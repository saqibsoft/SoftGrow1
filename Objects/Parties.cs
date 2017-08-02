using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Parties
    {
        private Int64 _PartyID;
        private string _PartyName;
        private string _CNICNo;
        private string _ContactNo;
        private string _City;
        private string _Email;
        private string _NTN;
        private string _Web;
        private string _Address;
        private string _AccountID;
        private bool _IsSupplier;
        private bool _IsCustomer;

        public Int64 PartyID { get { return _PartyID; } set { _PartyID = value; } }
        public string PartyName { get { return _PartyName; } set { _PartyName = value; } }
        public string CNICNo { get { return _CNICNo; } set { _CNICNo = value; } }
        public string ContactNo { get { return _ContactNo; } set { _ContactNo = value; } }
        public string City { get { return _City; } set { _City = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string NTN { get { return _NTN; } set { _NTN = value; } }
        public string Web { get { return _Web; } set { _Web = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string AccountID { get { return _AccountID; } set { _AccountID = value; } }
        public bool IsSupplier { get { return _IsSupplier; } set { _IsSupplier = value; } }
        public bool IsCustomer { get { return _IsCustomer; } set { _IsCustomer = value; } }
    }
}
