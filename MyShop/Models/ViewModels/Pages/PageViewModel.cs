using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models.Data;

namespace MyShop.Models.ViewModels.Pages
{
    public class PageViewModel
    {
        public PageViewModel()
        {

        }
        public PageViewModel(ShopPages row)
        {
            Id = row.Id;
            Title = row.Title;
            Link = row.Link;
            Content = row.Content;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;
            Timepage = row.Timepage;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }
        public string Link { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 4)]
        [AllowHtml]
        public string Content { get; set; }
        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }
        public DateTime Timepage { get; set; }
    }
}