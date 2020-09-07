using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.Order
{
    public class CartOrderViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get
            {
                return Price * Quantity;
            } }
        public string ImageProduct { get; set; }
    }
}