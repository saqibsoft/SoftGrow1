using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class SaleReturn
    {
        private Int64 _SaleReturnID;
        private Int64 _SaleID;
        private DateTime _EntryDate;
        private Int64 _CustomerID;
        private decimal _GrossValue;
        private decimal _SpecialDisc;
        private string _Narration;
        private int _UserID;
        private decimal _CashPaid;

        public Int64 SaleReturnID { get { return _SaleReturnID; } set { _SaleReturnID = value; } }
        public Int64 SaleID { get { return _SaleID; } set { _SaleID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public decimal GrossValue { get { return _GrossValue; } set { _GrossValue = value; } }
        public decimal SpecialDisc { get { return _SpecialDisc; } set { _SpecialDisc = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public decimal CashPaid { get { return _CashPaid; } set { _CashPaid = value; } }


    }
}
