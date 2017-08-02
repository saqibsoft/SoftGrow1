using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Employees
    {
        private Int64 _EmployeeID;
        private string _EmployeeName;
        private string _FatherName;
        private DateTime _JoiningDate;
        private string _CNIC;
        private string _ContactNo;
        private string _Address;
        private bool _IsSalesman;
        private decimal _BasicSalary;
        private int _UserID;
        private int _DepartmentID;
        private int _DesignationID;
        private int _ShiftID;
        private bool _InActive;
        private string _AccountNo;

        public Int64 EmployeeID { get { return _EmployeeID; } set { _EmployeeID = value; } }
        public string EmployeeName { get { return _EmployeeName; } set { _EmployeeName = value; } }
        public string FatherName { get { return _FatherName; } set { _FatherName = value; } }
        public DateTime JoiningDate { get { return _JoiningDate; } set { _JoiningDate = value; } }
        public string CNIC { get { return _CNIC; } set { _CNIC = value; } }
        public string ContactNo { get { return _ContactNo; } set { _ContactNo = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public bool IsSalesman { get { return _IsSalesman; } set { _IsSalesman = value; } }
        public decimal BasicSalary { get { return _BasicSalary; } set { _BasicSalary = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public int DepartmentID { get { return _DepartmentID; } set { _DepartmentID = value; } }
        public int DesignationID { get { return _DesignationID; } set { _DesignationID = value; } }
        public int ShiftID { get { return _ShiftID; } set { _ShiftID = value; } }
        public bool InActive { get { return _InActive; } set { _InActive = value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }

    }
}
