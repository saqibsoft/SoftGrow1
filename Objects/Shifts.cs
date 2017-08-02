using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class Shifts
    {
        private int _ShiftID;
        private string _ShiftTitle;

        public int ShiftID { get { return _ShiftID; } set { _ShiftID = value; } }
        public string ShiftTitle { get { return _ShiftTitle; } set { _ShiftTitle = value; } }

    }
}
