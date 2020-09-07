using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.Pages
{
    public class CommentsViewModel
    {
        public CommentsViewModel()
        {

        }
        public CommentsViewModel(Comments comments)
        {
            Id = comments.Id;
            AuthorName = comments.AuthorName;
            CommentText = comments.CommentText;
            ArticleId = comments.ArticleId;
            AdminConfirm = comments.AdminConfirm;
            DatePost = comments.DatePost;
            ArticleName = comments.ArticleName;
        }
        public int Id { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string CommentText { get; set; }
        [Required]
        public int ArticleId { get; set; }
        public bool AdminConfirm { get; set; }
        public DateTime DatePost { get; set; }
        public string ArticleName { get; set; }
    }
}