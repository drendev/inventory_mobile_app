using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_mobile_app.Models
{
    public class ProductListResponse
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public ProductList? Product { get; set; }
        public List<ProductList> Products { get; set; }
    }
}
