using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.Order
{
    public class UserOrdersViewModel
    {
        public int OrderId { get; set; }
        //public string UserName { get; set; }
        //public string UserLastName { get; set; }
        //public string UserMail { get; set; }
        //public string UserPhone { get; set; }
        //public bool Reistered { get; set; }
        //public bool Confirmed { get; set; }
        public decimal TotalCoast { get; set; }
        public Dictionary<string, int> ProductQuantity { get; set; }
        public DateTime DateOrder { get; set; }
    }
}