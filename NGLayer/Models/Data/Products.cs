using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Products")]

    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageString { get; set; }
        public string CategoryName { get; set; }
        //[ForeignKey]
        public int CategoryId { get; set; }
        public DateTime DateProduct { get; set; }
        public Products()
        {
            DateProduct = DateTime.Now;
        }
        [ForeignKey("CategoryId")]
        public virtual Categories Category { get; set; }
    }
}