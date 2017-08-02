using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class SaleBody
    {
        private Int64 _SerialNo;
        private Int64 _SaleID;
        private Int64 _ProductID;
        private decimal _Qty;
        private decimal _Price;
        private decimal _Discount;
        private decimal _TotalValue;
        private decimal _Cost;
        private String _Remarks;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 SaleID { get { return _SaleID; } set { _SaleID = value; } }
        public Int64 ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public decimal Qty { get { return _Qty; } set { _Qty = value; } }
        public decimal Price { get { return _Price; } set { _Price = value; } }
        public decimal Discount { get { return _Discount; } set { _Discount = value; } }
        public decimal TotalValue { get { return _TotalValue; } set { _TotalValue = value; } }
        public decimal Cost { get { return _Cost; } set { _Cost = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
