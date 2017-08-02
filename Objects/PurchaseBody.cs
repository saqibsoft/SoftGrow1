using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class PurchaseBody
    {
        private Int64 _SerialNo;
        private Int64 _PurchaseID;
        private Int64 _ProductID;
        private decimal _Qty;
        private decimal _Price;
        private decimal _Discount;
        private decimal _TotalValue;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 PurchaseID { get { return _PurchaseID; } set { _PurchaseID = value; } }
        public Int64 ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public decimal Qty { get { return _Qty; } set { _Qty = value; } }
        public decimal Price { get { return _Price; } set { _Price = value; } }
        public decimal Discount { get { return _Discount; } set { _Discount = value; } }
        public decimal TotalValue { get { return _TotalValue; } set { _TotalValue = value; } }
    }
}
