using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class DailyAttendance
    {
        private Int64 _AttendanceID;
        private Int64 _EmployeeID;
        private DateTime _EntryDate;
        private DateTime _AttendaceDateTime;
        private string _Remarks;
        private int _UserID;

        public Int64 AttendanceID { get { return _AttendanceID; } set { _AttendanceID = value; } }
        public Int64 EmployeeID { get { return _EmployeeID; } set { _EmployeeID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public DateTime AttendaceDateTime { get { return _AttendaceDateTime; } set { _AttendaceDateTime = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }


    }
}
