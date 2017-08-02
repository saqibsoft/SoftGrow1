using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Recovery
    {
        private Int64 _RecoveryID;
        private DateTime _RecoveryDate;
        private int _SalesmanID;
        private decimal _TotalRecovery;
        private int _UserID;
        private string _Remarks;

        public Int64 RecoveryID { get { return _RecoveryID; } set { _RecoveryID = value; } }
        public DateTime RecoveryDate { get { return _RecoveryDate; } set { _RecoveryDate = value; } }
        public int SalesmanID { get { return _SalesmanID; } set { _SalesmanID = value; } }
        public decimal TotalRecovery { get { return _TotalRecovery; } set { _TotalRecovery = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
