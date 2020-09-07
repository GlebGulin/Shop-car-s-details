using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Ourpages")]
    public class ShopPages
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Content { get; set; }
        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }
        public DateTime Timepage { get; set; }
        public ShopPages()
        {
            Timepage = DateTime.Now;
        }
    }
}