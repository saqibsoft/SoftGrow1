using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Sale
    {
       private Int64 _SaleID;
        private DateTime _EntryDate;
        private Int64 _CustomerID;
        private Int32 _SalesmanID;
        private decimal _GrossValue;
        private decimal _SpecialDisc;
        private string _Narration;
        private int _UserID;
        private decimal _CashReceived;

        public Int64 SaleID { get { return _SaleID; } set { _SaleID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public Int32 SalesmanID { get { return _SalesmanID; } set { _SalesmanID = value; } }
        public decimal GrossValue { get { return _GrossValue; } set { _GrossValue = value; } }
        public decimal SpecialDisc { get { return _SpecialDisc; } set { _SpecialDisc = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public decimal CashReceived { get { return _CashReceived; } set { _CashReceived = value; } }

    }
}
