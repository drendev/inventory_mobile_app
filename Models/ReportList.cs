using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class ReportList
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public Report? Report { get; set; }
        public List<Report> Reports { get; set; }
    }
}
