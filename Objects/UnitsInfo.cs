using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class UnitsInfo
    {
        private int _UnitID;
        private string _UnitTitle;

        public int UnitID { get { return _UnitID; } set { _UnitID = value; } }
        public string UnitTitle { get { return _UnitTitle; } set { _UnitTitle = value; } }
    }
}
