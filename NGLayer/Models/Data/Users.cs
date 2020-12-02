using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NGLayer.Models.Data
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string NickName { get; set; }
        public string Pass { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateRegistration { get; set; }
        public Users()
        {
            DateRegistration = DateTime.Now;
        }
    }
}