using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("OrdersAuthorized")]
    public class OrdersAuthorized
    {
        //public int Id { get; set; }
        [Key]
        public int OrderId { get; set; }
        
        public int UserId { get; set; }
        //public int ProductId { get; set; }
        public DateTime OrderCreated { get; set; }
        [ForeignKey("UserId")]
        public virtual Users us { get; set;}
    }
}