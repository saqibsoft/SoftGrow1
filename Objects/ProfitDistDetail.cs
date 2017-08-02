using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class ProfitDistDetail
    {
        private Int64 _DetailID;
        private Int32 _DistributionID;
        private Int64 _MemberID;
        private decimal _ProfitRate;
        private decimal _ProfitAmount;

        public Int64 DetailID { get { return _DetailID; } set { _DetailID = value; } }
        public Int32 DistributionID { get { return _DistributionID; } set { _DistributionID = value; } }
        public Int64 MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public decimal ProfitRate { get { return _ProfitRate; } set { _ProfitRate = value; } }
        public decimal ProfitAmount { get { return _ProfitAmount; } set { _ProfitAmount = value; } }
    }
}
