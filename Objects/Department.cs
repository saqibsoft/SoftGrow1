using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Department
    {
        private int _DepartmentID;
        private string _DepartmentTitle;

        public int DepartmentID { get { return _DepartmentID; } set { _DepartmentID = value; } }
        public string DepartmentTitle { get { return _DepartmentTitle; } set { _DepartmentTitle = value; } }


    }
}
