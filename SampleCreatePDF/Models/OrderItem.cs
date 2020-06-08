using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleCreatePDF.Models
{
    public class OrderItem
    {
        public string ProductNo { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPriceTl { get; set; }
        public decimal TotalPriceTl { get; set; }
        public string UnitName { get; set; }
    }
}