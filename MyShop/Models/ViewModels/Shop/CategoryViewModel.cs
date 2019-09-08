using MyShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.ViewModels.Shop
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {

        }
        public CategoryViewModel(Categories row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}