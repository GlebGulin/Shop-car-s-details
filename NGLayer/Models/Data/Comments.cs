using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Comments")]
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string CommentText { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public bool AdminConfirm { get; set; }
        public DateTime DatePost { get; set; }
        public Comments()
        {
            DatePost = DateTime.Now;
        }
        [ForeignKey("ArticleId")]
        public virtual ShopPages Article { get; set; }
    }
}