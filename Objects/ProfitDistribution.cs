using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class ProfitDistribution
    {
        private Int32 _DistributionID;
        private DateTime _DistributionDate;
        private int _SchemeID;
        private decimal _NetProfit;
        private string _Remarks;
        private int _UserID;

        public Int32 DistributionID { get { return _DistributionID; } set { _DistributionID = value; } }
        public DateTime DistributionDate { get { return _DistributionDate; } set { _DistributionDate = value; } }
        public int SchemeID { get { return _SchemeID; } set { _SchemeID = value; } }
        public decimal NetProfit { get { return _NetProfit; } set { _NetProfit = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public int UserID { get { return _UserID; } set { _UserID = value; } }
    }
}
