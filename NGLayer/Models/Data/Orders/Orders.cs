using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data.Orders
{
    [Table("AllOrders")]
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public bool Registered { get; set; }
        public bool Confirmed { get; set; }
        public DateTime DateOrder { get; set; }
        public Orders()
        {
            DateOrder = DateTime.Now;
        }
    }
}