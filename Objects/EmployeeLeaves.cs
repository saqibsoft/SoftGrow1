using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class EmployeeLeaves
    {
        private Int64 _LeaveID;
        private Int64 _EmployeeID;
        private DateTime _EntryDate;
        private DateTime _LeavFrom;
        private DateTime _LeaveTo;
        private string _Remarks;
        private int _UserID;
        private bool _Unpaid;

        public Int64 LeaveID { get { return _LeaveID; } set { _LeaveID = value; } }
        public Int64 EmployeeID { get { return _EmployeeID; } set { _EmployeeID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public DateTime LeavFrom { get { return _LeavFrom; } set { _LeavFrom = value; } }
        public DateTime LeaveTo { get { return _LeaveTo; } set { _LeaveTo = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public bool Unpaid { get { return _Unpaid; } set { _Unpaid = value; } }

    }
}
