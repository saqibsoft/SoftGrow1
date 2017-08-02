using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class Products
    {
        private Int64 _ProductID;
        private string _ProductName;
        private decimal _CostPrice;
        private decimal _SalePrice;
        private int _UnitID;
        private decimal _OpeningStock;
        private decimal _OpeningStockValue;
        private bool _IsRawMaterial;

        public Int64 ProductID { get { return _ProductID; } set { _ProductID = value; } }
        public string ProductName { get { return _ProductName; } set { _ProductName = value; } }
        public decimal CostPrice { get { return _CostPrice; } set { _CostPrice = value; } }
        public decimal SalePrice { get { return _SalePrice; } set { _SalePrice = value; } }
        public int UnitID { get { return _UnitID; } set { _UnitID = value; } }
        public decimal OpeningStock { get { return _OpeningStock; } set { _OpeningStock = value; } }
        public decimal OpeningStockValue { get { return _OpeningStockValue; } set { _OpeningStockValue = value; } }
        public bool IsRawMaterial { get { return _IsRawMaterial; } set { _IsRawMaterial = value; } }
    }
}
