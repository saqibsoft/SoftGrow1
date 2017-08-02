using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class StockSamples
    {
        private Int64 _SampleID;
        private DateTime _IssueDate;
        private Int64 _CustomerID;
        private int _UserID;
        private string _Remarks;

        public Int64 SampleID { get { return _SampleID; } set { _SampleID = value; } }
        public DateTime IssueDate { get { return _IssueDate; } set { _IssueDate = value; } }
        public Int64 CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
    }
}
