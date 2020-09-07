using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyShop.Models.Data
{
    [Table("Sidebar")]
    public class Sidebar
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}