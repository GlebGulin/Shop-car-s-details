using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Reviews")]
    public class Reviews
    {
        [Key]
        public int Id { get; set; }
        public string NameAuthor { get; set; }
        public string ReviewText { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool AdminConfirm { get; set; }
        public DateTime DatePost { get; set; }
        public Reviews()
        {
            DatePost = DateTime.Now;
        }
        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }
    }
}