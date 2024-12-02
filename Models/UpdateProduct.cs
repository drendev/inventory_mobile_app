using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class UpdateProduct
    {
        // public string? ProductId { get; set; }
        public string? Barcode { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? ExpiryDate { get; set; }
    }
}
