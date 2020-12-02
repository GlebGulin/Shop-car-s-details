using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.YourAccount
{
    public class YourProfileViewModel
    {
        public YourProfileViewModel()
        {

        }
        public YourProfileViewModel(Users u)
        {
            Id = u.Id;
            FirstName = u.FirstName;
            SurName = u.SurName;
            Mail = u.Mail;
            NickName = u.NickName;
            Pass = u.Pass;
            PhoneNumber = u.PhoneNumber;
            DateRegistration = u.DateRegistration;

        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string SurName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        [Required]
        //???
        [DataType(DataType.Password)]
        public string Pass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}