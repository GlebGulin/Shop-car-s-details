using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.Shop
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel()
        {

        }
        public ReviewsViewModel(Reviews r)
        {
            Id = r.Id;
            NameAuthor = r.NameAuthor;
            ReviewText = r.ReviewText;
            ProductId = r.ProductId;
            AdminConfirm = r.AdminConfirm;
            DatePost = r.DatePost;
            ProductName = r.ProductName;

        }
        public int Id { get; set; }
        [Required]
        public string NameAuthor { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public int ProductId { get; set; }
        public bool AdminConfirm { get; set; }
        public DateTime DatePost { get; set; }
        public string ProductName { get; set; }
       
    }
}