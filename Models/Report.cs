using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class Report
    {
        public string? ReportId { get; set; }
        public string? ProductName { get; set; }
        public string? Username { get; set; }
        public DateOnly Date { get; set; }
        public int Quantity { get; set; }
        public int CurrentStock { get; set; }
        public string? ReportType { get; set; }
        public TimeOnly Created { get; set; }
    }
}
