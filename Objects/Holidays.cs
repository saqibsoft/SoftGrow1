using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Holidays
    {
        private Int64 _HolidayID;
        private DateTime _HolidayDate;
        private string _Reason;
        private int _UserID;

        public Int64 HolidayID { get { return _HolidayID; } set { _HolidayID = value; } }
        public DateTime HolidayDate { get { return _HolidayDate; } set { _HolidayDate = value; } }
        public string Reason { get { return _Reason; } set { _Reason = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }


    }
}
