using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class VoucherHeader
    {
        private Int64 _VoucherID;
        private string _VoucherType;
        private DateTime _VoucherDate;
        private string _Narration;
        private bool _IsPosted;
        private int _UserID;
        private DateTime _EntryDate;
        private string _PrintVoucherType;

        public Int64 VoucherID { get { return _VoucherID; } set { _VoucherID = value; } }
        public string VoucherType { get { return _VoucherType; } set { _VoucherType = value; } }
        public DateTime VoucherDate { get { return _VoucherDate; } set { _VoucherDate = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public bool IsPosted { get { return _IsPosted; } set { _IsPosted = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public string PrintVoucherType { get { return _PrintVoucherType; } set { _PrintVoucherType = value; } }
    }
}
