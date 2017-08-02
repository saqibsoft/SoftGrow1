using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class VoucherBody
    {
        private Int64 _SerialNo;
        private Int64 _VoucherID;
        private string _VoucherType;
        private string _AccountNo;
        private string _Remarks;
        private decimal _Debit;
        private decimal _Credit;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 VoucherID { get { return _VoucherID; } set { _VoucherID = value; } }
        public string VoucherType { get { return _VoucherType; } set { _VoucherType = value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public decimal Debit { get { return _Debit; } set { _Debit = value; } }
        public decimal Credit { get { return _Credit; } set { _Credit = value; } }
    }
}
