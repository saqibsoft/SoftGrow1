using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class ProductionBody
    {
        private Int64 _SerialNo;
        private Int64 _ProductionID;
        private Int64 _ProductID;
        private decimal _Qty;
        private decimal _Cost;
        private string _Remarks;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 ProductionID { get { return _ProductionID; } set { _ProductionID = value; } }
        public Int64 ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public decimal Qty { get { return _Qty; } set { _Qty = value; } }
        public decimal Cost { get { return _Cost; } set { _Cost = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
