using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Sidebar")]
    public class SideBar
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}