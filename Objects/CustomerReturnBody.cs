using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class CustomerReturnBody
    {
        private Int64 _SerialNo;
        private Int64 _ReturnID;
        private Int64 _ProductID;
        private decimal _Qty;
        private decimal _Cost;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 ReturnID { get { return _ReturnID; } set { _ReturnID = value; } }
        public Int64 ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public decimal Qty { get { return _Qty; } set { _Qty = value; } }
        public decimal Cost { get { return _Cost; } set { _Cost = value; } }

    }
}
