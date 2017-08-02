using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Salary
    {
        private Int64 _SalaryID;
        private DateTime _GenerateDate;
        private DateTime _SalaryMonth;
        private int _DepartmentID;
        private int _DesignationID;
        private int _ShiftID;
        private string _Remarks;
        private decimal _TotalSalary;
        private int _UserID;

        public Int64 SalaryID { get { return _SalaryID; } set { _SalaryID = value; } }
        public DateTime GenerateDate { get { return _GenerateDate; } set { _GenerateDate = value; } }
        public DateTime SalaryMonth { get { return _SalaryMonth; } set { _SalaryMonth = value; } }
        public int DepartmentID { get { return _DepartmentID; } set { _DepartmentID = value; } }
        public int DesignationID { get { return _DesignationID; } set { _DesignationID = value; } }
        public int ShiftID { get { return _ShiftID; } set { _ShiftID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public decimal TotalSalary { get { return _TotalSalary; } set { _TotalSalary = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }

    }
}
