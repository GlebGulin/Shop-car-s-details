﻿using NGLayer.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGLayer.Models.ViewModels.Pages
{
    public class SidebarViewModel
    {
        public SidebarViewModel()
        {

        }
        public SidebarViewModel(SideBar row)
        {
            Id = row.Id;
            Content = row.Content;
        }
        public int Id { get; set; }
        [Required]
        //[StringLength(50, MinimumLength = 4)]
        [AllowHtml]
        public string Content { get; set; }

    }
}