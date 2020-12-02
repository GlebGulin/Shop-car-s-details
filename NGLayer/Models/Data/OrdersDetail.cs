using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("OrdersDetail")]
    public class OrdersDetail
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrdersAuthorized ordersAuthorized { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public  Users users { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
        public int Quantity { get; set; }
    }
}