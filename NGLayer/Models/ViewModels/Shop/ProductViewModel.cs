using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGLayer.Models.ViewModels.Shop
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(Products row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Description = row.Description;
            Price = row.Price;
            CategoryName = row.CategoryName;
            ImageString = row.ImageString;
            //DateProduct = row.DateProduct;

        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        public string ImageString { get; set; }
        public string CategoryName { get; set; }
        [Required]

        public int CategoryId { get; set; }
        //public DateTime DateProduct { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<string> MyPictures { get; set; }


        //public virtual Categories Category { get; set; }
    }
}