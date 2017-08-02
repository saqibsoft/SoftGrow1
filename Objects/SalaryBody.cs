using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class SalaryBody
    {
        private Int64 _SerialNo;
        private Int64 _SalaryID;
        private Int64 _EmployeeID;
        private decimal _Presents;
        private decimal _Leaves;
        private decimal _BasicSalary;
        private decimal _Allowances;
        private decimal _Bonus;
        private decimal _Deduction;
        private decimal _NetSalary;

        public Int64 SerialNo { get { return _SerialNo; } set { _SerialNo = value; } }
        public Int64 SalaryID { get { return _SalaryID; } set { _SalaryID = value; } }
        public Int64 EmployeeID { get { return _EmployeeID; } set { _EmployeeID = value; } }
        public decimal Presents { get { return _Presents; } set { _Presents = value; } }
        public decimal Leaves { get { return _Leaves; } set { _Leaves = value; } }
        public decimal BasicSalary { get { return _BasicSalary; } set { _BasicSalary = value; } }
        public decimal Allowances { get { return _Allowances; } set { _Allowances = value; } }
        public decimal Bonus { get { return _Bonus; } set { _Bonus = value; } }
        public decimal Deduction { get { return _Deduction; } set { _Deduction = value; } }
        public decimal NetSalary { get { return _NetSalary; } set { _NetSalary = value; } }
    }
}
