using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Production
    {
        private Int64 _ProductionID;
        private DateTime _EntryDate;
        private string _Narration;
        private int _UserID;

        public Int64 ProductionID { get { return _ProductionID; } set { _ProductionID = value; } }
        public DateTime EntryDate { get { return _EntryDate; } set { _EntryDate = value; } }
        public string Narration { get { return _Narration; } set { _Narration = value; } }
        public int UserID { get { return _UserID; } set { _UserID  = value; } }

    }
}
