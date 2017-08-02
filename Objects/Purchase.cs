using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Purchase
    {
        private Int64 _PurchaseID;
        private DateTime _EntryDate;
        private Int64 _VendorID;
        private decimal _GrossValue;
        private string _Narration;
        private int _UserID;
        private decimal _CashPaid;

        public Int64 PurchaseID { get { return _PurchaseID; } set { _PurchaseID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public Int64 VendorID { get { return _VendorID; } set { _VendorID = value; } }
        public decimal GrossValue { get { return _GrossValue; } set { _GrossValue = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public decimal CashPaid { get { return _CashPaid; } set { _CashPaid = value; } }
    }
}
