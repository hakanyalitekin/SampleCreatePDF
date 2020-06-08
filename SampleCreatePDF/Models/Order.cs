using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleCreatePDF.Models
{
    public class Order
    {
        public string OrderNo { get; set; }
        public DateTime DateDelivered { get; set; }
        public DateTime CreateTs { get; set; }
        public string Note { get; set; }
        public decimal TotalPriceTl { get; set; }


        public List<OrderItem> OrderItems { get; set; }
    }
}