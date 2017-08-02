using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class ShareScheme
    {
        private int _SchemeID;
        private string _SchemeTitle;
        private DateTime _EntryDate;
        private Int64 _TotalShares;
        private decimal _PerShareValue;
        private string _Remarks;
        private int _UserID;

        public int SchemeID { get { return _SchemeID; } set { _SchemeID = value; } }
        public string SchemeTitle { get { return _SchemeTitle; } set { _SchemeTitle = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public Int64 TotalShares { get { return _TotalShares; } set { _TotalShares = value; } }
        public decimal PerShareValue { get { return _PerShareValue; } set { _PerShareValue = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
    }
}
