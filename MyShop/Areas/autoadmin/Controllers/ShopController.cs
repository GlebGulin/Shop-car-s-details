using MyShop.Content;
using MyShop.Models.Data;
using MyShop.Models.ViewModels.Shop;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyShop.Areas.autoadmin.Controllers
{
    public class ShopController : Controller
    {
        // GET: autoadmin/Shop
        public ActionResult CategorList()
        {
            List<CategoryViewModel> categorylist;
            using (Db db = new Db())
            {
                categorylist = db.categories.ToArray()
                    .OrderBy(x => x.Sorting)
                    .Select(x => new CategoryViewModel(x)).ToList();

            }
            return View(categorylist);
        }
        [HttpPost]
        public string AddNewCategory(string CatValue)
        {
            string id;
            using (Db db = new Db())
            {
                if (db.categories.Any(x => x.Name == CatValue))
                {
                    return "titletaken";
                }
                Categories newcat = new Categories();
                newcat.Name = CatValue;
                string newlink;
                newlink = CatValue.ToLower();
                Translitor translitor = new Translitor();
                foreach (KeyValuePair<string, string> pair in translitor._words)
                {
                    newlink = newlink.Replace(pair.Key, pair.Value);

                }
                newcat.Sorting = 100;
                newcat.Slug = newlink;
                //newcat.Id = id;
                db.categories.Add(newcat);
                db.SaveChanges();
                id = newcat.Id.ToString();
                
            }
            return id;
            //return View();
        }
        public ActionResult DelCat(int id)
        {
            using (Db db = new Db())
            {
                Categories categories=db.categories.Find(id);
                db.categories.Remove(categories);
                db.SaveChanges();

            }
            TempData["DelCat"] = "Категория успешно удалена";

            return RedirectToAction("CategorList");
        }
        [HttpPost]
        public void UpdateCat(int[] id)
        {
           
            using (Db db = new Db())
            {
                Categories categories;
                int count = 1;
                foreach (var catId in id)
                {
                    categories = db.categories.Find(catId);
                    categories.Sorting = count;
                    db.SaveChanges();
                    count++;
                }

            }
                
        }
        [HttpPost]
        public string ChangeCategory(string newmytextcat, int id)
        {
            using (Db db = new Db())
            {
                Categories changecat = db.categories.Find(id);
                if (db.categories.Any(x => x.Name == newmytextcat))
                {
                    return "titletaken";
                }
                changecat.Name = newmytextcat;
                string newslug;
                newslug = newmytextcat.ToLower();
                Translitor translitor = new Translitor();
                foreach (KeyValuePair<string, string> pair in translitor._words)
                {
                    newslug = newslug.Replace(pair.Key, pair.Value);

                }
                changecat.Slug = newslug;
                db.SaveChanges();
            }
                return newmytextcat;
        }
        [HttpGet]
        public ActionResult AddNewProduct()
        {
            ProductViewModel newprod = new ProductViewModel();
            //if (ModelState.IsValid)
            //{
                using (Db db = new Db())
                {
                    newprod.Categories = new SelectList(db.categories.ToList(), "Id", "Name");
                }
            //}
            return View(newprod);
        }
        [HttpPost]
        public ActionResult AddNewProduct(ProductViewModel newprod, HttpPostedFileBase newfile)
        //public ActionResult AddNewProduct(ProductViewModel newprod, )
        {
            if (!ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    newprod.Categories = new SelectList(db.categories.ToList(), "Id", "Name");
                    return View(newprod);
                }

            }
            int id;
            using (Db db = new Db())
            {
                Products newpr = new Products();
                newpr.Name = newprod.Name;
                string linkprod;
                if (string.IsNullOrWhiteSpace(newprod.Slug))
                {

                    linkprod = newprod.Name.ToLower();
                    Translitor translitor = new Translitor();
                    foreach (KeyValuePair<string, string> pair in translitor._words)
                    {
                        linkprod = linkprod.Replace(pair.Key, pair.Value);

                    }
                }
                else
                {
                    linkprod = newprod.Slug.ToLower();
                    Translitor translitor = new Translitor();
                    foreach (KeyValuePair<string, string> pair in translitor._words)
                    {
                        linkprod = linkprod.Replace(pair.Key, pair.Value);

                    }
                }
                if (db.product.Any(x => x.Name == newprod.Name) || db.shoppages.Any(x => x.Link == newprod.Slug))
                {
                    newprod.Categories = new SelectList(db.categories.ToList(), "Id", "Name");

                    ModelState.AddModelError("Такое название страницы или ссылка уже существуют ", " ");
                    return View(newprod);
                }
                newpr.Slug = linkprod;
                newpr.Price = newprod.Price;
                newpr.Description = newprod.Description;
                newpr.CategoryId = newprod.CategoryId;
                Categories newcat = db.categories.FirstOrDefault(x => x.Id == newprod.CategoryId);
                newpr.CategoryName = newcat.Name;
                db.product.Add(newpr);
                db.SaveChanges();
                id = newpr.Id;
            }
            TempData["SMM"] = "Успешно добавлено";
            #region UploadPhoto
            //Directory for photos
            var imagedirectory = new DirectoryInfo(string.Format("{0}Content\\images\\Upload", Server.MapPath(@"\")));
            //var imagedirectory = new DirectoryInfo();


            var namepicture1 = Path.Combine(imagedirectory.ToString(), "Products");
            var namepicture2 = Path.Combine(imagedirectory.ToString(), "Products\\" + id.ToString());
            var namepicture3 = Path.Combine(imagedirectory.ToString(), "Products\\" + id.ToString() + "\\Thumbs");
            var namepicture4 = Path.Combine(imagedirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery");
            var namepicture5 = Path.Combine(imagedirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thumbs");
            if (!Directory.Exists(namepicture1))
            {
                Directory.CreateDirectory(namepicture1);
            }
            if (!Directory.Exists(namepicture2))
            {
                Directory.CreateDirectory(namepicture2);
            }
            if (!Directory.Exists(namepicture3))
            {
                Directory.CreateDirectory(namepicture3);
            }
            if (!Directory.Exists(namepicture4))
            {
                Directory.CreateDirectory(namepicture4);
            }
            if (!Directory.Exists(namepicture5))
            {
                Directory.CreateDirectory(namepicture5);
            }
            //Checking upload picture
            if (newfile != null && newfile.ContentLength > 0)
            {
                //Get extention of file
                var ext = newfile.ContentType.ToLower();
                //check the extention
                if (ext != "image/jpg" && 
                    ext != "image/png" &&
                    ext != "image/jpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/pjpeg" )
                {
                    using (Db db = new Db())
                    {
                        newprod.Categories = new SelectList(db.categories.ToList(), "Id", "Name");
                        ModelState.AddModelError(" ", "Неверный формат картинки");
                        return View(newprod);
                    }

                }
                //Init name of picture
                string imgName = newfile.FileName;
                using (Db db = new Db())
                {
                    Products newpr = db.product.Find(id);
                    newpr.ImageString = imgName;
                    db.SaveChanges();
                }
                //Set unique filestring and extention
                var path = string.Format("{0}\\{1}", namepicture2, imgName);
                var path2 = string.Format("{0}\\{1}", namepicture3, imgName);
                //var path3 = string.Format("{0}\\{1}", namepicture3, imgName);
                //var path4 = string.Format("{0}\\{1}", namepicture4, imgName);
                //var path5 = string.Format("{0}\\{1}", namepicture5, imgName);
                newfile.SaveAs(path);
                WebImage img = new WebImage(newfile.InputStream);
                img.Resize(200, 300);
                img.Save(path2);

            }
            #endregion

            return RedirectToAction("AddNewProduct");
        }
        public ActionResult Products(int? page, int? catId)
        {
            //Get the list of product View Mofels
            List<ProductViewModel> allproducts;
            //Number of page using pagedList (GitHub)
            var numberPage = page ?? 1;
            using (Db db = new Db())
            {
                allproducts = db.product.ToArray()
                    .Where(x => catId == null || catId == 0 || x.CategoryId == catId)
                    .Select(x => new ProductViewModel(x))
                    .ToList();
                //Include list of categories
                ViewBag.Categories = new SelectList(db.categories.ToList(), "Id", "Name");
                //Set selected category
                ViewBag.SelectedCat = catId.ToString();
            }
            //Pagination for pages
            var onePageOfProducts = allproducts.ToPagedList(numberPage, 5);
            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View(allproducts);
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductViewModel productViewModel;
            using (Db db = new Db())
            {
                
                Products editprod = db.product.Find(id);
                if (editprod == null)
                {
                    return Content("Товара не существует");
                }
                productViewModel = new ProductViewModel(editprod);
                productViewModel.Categories = new SelectList(db.categories.ToList(), "Id", "Name");
                productViewModel.MyPictures = Directory.EnumerateFiles(Server.MapPath("~/Content/images/Upload/Products/" + id + "/Gallery/Thumbs"))
                    .Select(y => Path.GetFileName(y));

            }
            return View(productViewModel);
            
        }
    //    [HttpPost]
    //    public ActionResult EditProduct(ProductViewModel)
    //    {

    //    }
         public ActionResult DeleteProduct(int id)
        {
            using (Db db = new Db())
            {
                Products delprod = db.product.Find(id);
                db.product.Remove(delprod);
                db.SaveChanges();
            }
            TempData["DeleteProd"] = "Успешно удалено";
            return RedirectToAction("Products");
        }
    }
}