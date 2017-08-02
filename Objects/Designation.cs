using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
  public  class Designation
    {
        private int _DesignationID;
        private string _DesignationTitle;

        public int DesignationID { get { return _DesignationID; } set { _DesignationID = value; } }
        public string DesignationTitle { get { return _DesignationTitle; } set { _DesignationTitle = value; } }
    }
}
