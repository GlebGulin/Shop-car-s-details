using Binding.Models;
using NGLayer.Models.Data;
using NGLayer.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
         
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //ulr page/{string page}
        //Page layout for Blog
        public ActionResult Index(string page = "")

        {
            //Check empty page and get slug
            if (page == "") page = "home";
            PageViewModel pgVM;
            ShopPages shPg;
            //Check page exist
            using (Db db = new Db())
            {
                if (! db.shoppages.Any(x => x.Link.Equals(page)))
                {
                    return RedirectToAction("Page", new { page = "" });
                }
            }
            //Get model of page

            using (Db db = new Db())
            {
                shPg = db.shoppages.Where(x => x.Link == page).FirstOrDefault();
            }
            //Set title
            ViewBag.PageTitle = shPg.Title;
            //Check sidebar exist
            if (shPg.HasSidebar == true)
            {
                ViewBag.SideBar = "YES"; 
            }
            else
            {
                ViewBag.SideBar = "NO";
            }
            pgVM = new PageViewModel(shPg);

                return View(pgVM);
        }
        //[HttpGet]
        public ActionResult PageMenuPatial()
        {
            //List<PageViewModel> pgList;
            //using (Db db = new Db())
            //{
            //    pgList = db.shoppages.ToArray().OrderBy(x => x.Sorting).Where(x => x.Link != "home").Select(x => new PageViewModel()).ToList();
            //}
            //    return PartialView(pgList);

            List<PageViewModel> pageViewModels;
            using (Db db = new Db())
            {

                pageViewModels = db.shoppages.ToArray().OrderBy(x => x.Sorting).Where(x =>x.Link !="home").Select(x => new PageViewModel(x)).ToList();
            }
            return PartialView(pageViewModels);
        }
        [HttpGet]
        public ActionResult ClientCommentPartial(int id)
        {
            List<CommentsViewModel> comVM;
            using (Db db = new Db())
            {

                comVM = db.comments.ToArray().OrderBy(x => x.DatePost).Where(x => x.ArticleId == id && x.AdminConfirm == true).Select(x => new CommentsViewModel(x)).ToList();
            }
            return PartialView(comVM);
        }
        [HttpGet]
        public ActionResult SidebarPartial()
        {
            SidebarViewModel sdbVM;
            using (Db db = new Db())
            {
                SideBar sd = db.sidebar.Find(1);
                sdbVM = new SidebarViewModel(sd);
            }
            return PartialView(sdbVM);
        }
    }
}