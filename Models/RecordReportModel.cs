using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class RecordReportModel
    {
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public string? UserName { get; set; }
        public int? CurrentStock { get; set; }
        public string? ReportType { get; set; }
    }
}