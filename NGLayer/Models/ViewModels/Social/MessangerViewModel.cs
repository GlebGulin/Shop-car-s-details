using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGLayer.Models.ViewModels.Social
{
    public class MessangerViewModel
    {
        public MessangerViewModel()
        {

        }
        public MessangerViewModel(Messangers mess)
        {
            Id = mess.Id;
            NameChanel = mess.NameChanel;
            ApiKEY = mess.ApiKEY;

        }
        public int Id { get; set; }
        [Required]
        public string NameChanel { get; set; }
        [Required]
        public string ApiKEY { get; set; }
    }
}