using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("UserRole")]

    public class UserRole
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int RoleId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
    }
}