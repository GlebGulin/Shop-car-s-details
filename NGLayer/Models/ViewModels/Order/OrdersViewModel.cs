using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NGLayer.Models.Data.Orders;

namespace NGLayer.Models.ViewModels.Order
{
    public class OrdersViewModel
    {
        public OrdersViewModel()
        {
                
        }
        public OrdersViewModel(Orders order)
        {
            OrderId = order.OrderId;
            Name = order.Name;
            SurName = order.SurName;
            Mail = order.Mail;
            Phone = order.Phone;
            Registered = order.Registered;
            DateOrder = order.DateOrder;
            Confirmed = order.Confirmed;

        }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public bool Registered { get; set; }
        public bool Confirmed { get; set; }
        public DateTime DateOrder { get; set; }
    }
}