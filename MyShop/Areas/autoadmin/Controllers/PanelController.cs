﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.autoadmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
       
        // GET: autoadmin/Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}