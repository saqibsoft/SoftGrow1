using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class RecoveryBody
    {
        private Int64 _SerialNo;
        private Int64 _RecoveryID;
        private Int64 _CustomerID;
        private decimal _AmountRecoverd;
        private string _Remarks;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 RecoveryID { get { return _RecoveryID; } set { _RecoveryID = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public decimal AmountRecoverd { get { return _AmountRecoverd; } set { _AmountRecoverd = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
