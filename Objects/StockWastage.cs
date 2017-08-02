using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public  class StockWastage
    {
        private Int64 _WasteID;
        private DateTime _EntryDate;
        private decimal _TotalValue;
        private int _UserID;
        private string _Remarks;

        public Int64 WasteID { get { return _WasteID; } set { _WasteID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public decimal TotalValue { get { return _TotalValue; } set { _TotalValue = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
