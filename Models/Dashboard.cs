using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class Dashboard
    {
        public int? TotalProducts { get; set; }
        public int? TotalStocks { get; set; }
        public int? TopSelling { get; set; }
        public int? LowStocksProducts { get; set; }
        public int InStock { get; set; }
        public int LowStock { get; set; }
        public int OutOfStock { get; set; }
        public string? LowStockProduct { get; set; }
        public string? OutOfStockProduct { get; set; }
    }
}
