using System;
using System.Collections.Generic;
using System.Text;

namespace CFSIS.Shared.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailNo { get; set; }
        public int OrderNo { get; set; }
        public string ItemName { get; set; }
        public string Notes { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }
}
