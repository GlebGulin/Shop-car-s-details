//using MyShop.Models.Data;
using NGLayer.Models.Data;
using NGLayer.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Content;
using Binding.Models;

namespace MyShop.Areas.autoadmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPagesController : Controller
    {
        
        // GET: autoadmin/Adminpages
        [HttpGet]
        public ActionResult Index()
        {
            
            List<PageViewModel> pageViewModels;
            using (Db db = new Db())
            {
                
                pageViewModels = db.shoppages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageViewModel(x)).ToList();
            }
                return View(pageViewModels);
        }
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPage(PageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            using (Db db = new Db())
            {
                string link;
                
                ShopPages shopPages = new ShopPages();
                shopPages.Title = model.Title;
                
                if(string.IsNullOrWhiteSpace(model.Link))
                {

                   
                    link = model.Title.ToLower();
                    Translitor translitor = new Translitor();
                    foreach (KeyValuePair<string, string> pair in translitor._words)
                    {
                        link = link.Replace(pair.Key, pair.Value);
                        
                    }
                }
                else
                {
                    //link = model.Link.ToLower().Replace(" ", "-").Replace("а", "a").Replace("б", "b")
                    //     .Replace("в", "v").Replace("г", "g").Replace("д", "d")
                    //    .Replace("е", "e").Replace("ж", "zh").Replace("з", "z").Replace("и", "i").Replace("й", "y").Replace("к", "k")
                    //    .Replace("л", "l").Replace("м", "m").Replace("н", "n").Replace("о", "o").Replace("п", "p").Replace("р", "r")
                    //    .Replace("с", "s").Replace("т", "t").Replace("у", "u").Replace("ф", "f").Replace("х", "h").Replace("ц", "c")
                    //    .Replace("ч", "ch").Replace("ш", "sh").Replace("ю", "u").Replace("я", "ya").Replace("ь", "").Replace("ъ", "");

                    //link = model.Link.Replace(" ", "-").ToLower();
                    link = model.Link.ToLower();
                    Translitor translitor = new Translitor();
                    foreach (KeyValuePair<string, string> pair in translitor._words)
                    {
                        link = link.Replace(pair.Key, pair.Value);
                        
                    }

                }
                if (db.shoppages.Any(x=>x.Title == model.Title) || db.shoppages.Any( x=> x.Link == model.Link))
                {
                    ModelState.AddModelError(" ", "Такое название страницы или ссылка уже существуют ");
                    return View(model);
                }
                shopPages.Content = model.Content;
                
                shopPages.Link = link;
                shopPages.HasSidebar = model.HasSidebar;
                shopPages.Sorting = 100;

                db.shoppages.Add(shopPages);
                db.SaveChanges();
                

            }
            TempData["SM"] = "Успешно добавлено";
            return RedirectToAction("AddPage");

        }
        [HttpGet]
        public ActionResult Edit (int id)
        {
            PageViewModel pageViewModel;
            using (Db db = new Db())
            {
                ShopPages shopPages = db.shoppages.Find(id);
                if (shopPages == null)
                {
                    return Content("Страница не создана либо удалена");
                }
                pageViewModel = new PageViewModel(shopPages);
            }
           
            return View(pageViewModel);
        }
        [HttpPost]
        public ActionResult Edit(PageViewModel pageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pageViewModel);
            }

            using (Db db = new Db())
                {
                int id = pageViewModel.Id;
                string link = "home";
                ShopPages shop = db.shoppages.Find(id);
                
                shop.Title = pageViewModel.Title;
                if (pageViewModel.Link != "home")
                {
                    if (string.IsNullOrWhiteSpace(pageViewModel.Link))
                    {
                        link = pageViewModel.Title.ToLower();
                        Translitor translitor = new Translitor();
                        foreach (KeyValuePair<string, string> pair in translitor._words)
                        {
                            link = link.Replace(pair.Key, pair.Value);

                        }
                    }
                    else
                    {

                        link = pageViewModel.Link.ToLower();
                        Translitor translitor = new Translitor();
                        foreach (KeyValuePair<string, string> pair in translitor._words)
                        {
                            link = link.Replace(pair.Key, pair.Value);

                        }

                    }
                    //Проверим на повторяющие Title или Link из всего спсика
                    if(db.shoppages.Where(x=> x.Id != id).Any(x=> x.Title == pageViewModel.Title) ||
                        (db.shoppages.Where(x=> x.Id != id).Any(x=> x.Link == pageViewModel.Link)))
                    {
                        //return Content("Страница с такой ссылкой или названием уже существует");
                        //ModelState.AddModelError(" ", "Страница с такой ссылкой или названием уже существует");
                        TempData["Error"] = "Error";
                        return View(pageViewModel);
                        //return RedirectToAction("Edit");
                    }

                    shop.Link = link;
                }
                shop.Content = pageViewModel.Content;
                shop.Title = pageViewModel.Title;
                
                shop.HasSidebar = pageViewModel.HasSidebar;
                
                //db.shoppages.Add(shop);
                db.SaveChanges();
            }
           

            

            TempData["SM"] = "Изменения сохранены";
            return RedirectToAction("Edit");
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            PageViewModel model;
            using (Db db = new Db())
            {

                ShopPages shop = db.shoppages.Find(id);
                if (shop == null)
                {
                    return Content(" ", "Страница не создана или удалена");
                }
                model = new PageViewModel(shop);
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            //PageViewModel model;
            using (Db db = new Db())
            {
                ShopPages shop = db.shoppages.Find(id);
                //model = new PageViewModel(shop);
                db.shoppages.Remove(shop);
                db.SaveChanges();
                
            }

            
            
            
            TempData["Del"] = "Запись успешно удалена";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public void RewriteList (int[] id)
        {
            using (Db db = new Db())
            {
                //for (int i = 0; i<id.Length;i++)
                //{

                //}
                int count = 1;
                ShopPages shops;
                foreach (var pageId in id)
                {
                    shops = db.shoppages.Find(pageId);
                    shops.Sorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }
        [HttpGet]
        public ActionResult EditSidebar()
        {
            SidebarViewModel model;
            using (Db db = new Db())
            {
                SideBar sidebar = db.sidebar.Find(1);
                model = new SidebarViewModel(sidebar);

            }
        return View(model);
        }
        [HttpPost]
        public ActionResult EditSidebar(SidebarViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db = new Db())
            {
                SideBar sidebar = db.sidebar.Find(1);
                sidebar.Content = model.Content;
                db.SaveChanges();
            }

            TempData["Updt"] = "Изменения в боковой панели сохранены";
            return RedirectToAction("EditSidebar");

        }
        //Get list of all comments
        [HttpGet]
        public ActionResult CommentList()
        {
            List<CommentsViewModel> comVM;
            using (Db db = new Db())
            {

                comVM = db.comments.ToArray().OrderBy(x => x.DatePost).Select(x => new CommentsViewModel(x)).ToList();
                foreach (var item in comVM)
                {
                    ShopPages p = db.shoppages.Find(item.ArticleId);
                    item.ArticleName = p.Title;
                }
            }
            return View(comVM);
        }
    }
}