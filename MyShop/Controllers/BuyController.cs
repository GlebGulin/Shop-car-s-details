using Binding.Models;
using NGLayer.Models.Data;
using NGLayer.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace MyShop.Controllers
{
    public class BuyController : Controller
    {
        // GET: Buy
        public ActionResult Index()
        {

            
            return RedirectToAction("Index", "Page");
        }
        //[HttpGet]
        public ActionResult CategoryMenuPartial()
        {
            List<CategoryViewModel> ctv;
            //Categories categories;
            using (Db db = new Db())
            {
                ctv = db.categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryViewModel(x)).ToList();
            }
                return PartialView(ctv);
        }
        //[HttpGet]
        public ActionResult Category(string body)
        {
            List<ProductViewModel> pVM;
            //Find id of category
            if (body != null)
            {
                using (Db db = new Db())
                {
                    Categories category = db.categories.Where(x => x.Slug == body).FirstOrDefault();
                    int categoryId = category.Id;
                    pVM = db.product.ToArray().Where(x => x.CategoryId == categoryId).OrderBy(x => x.Name).Select(x => new ProductViewModel(x)).ToList();
                    var productGroup = db.product.Where(x => x.CategoryId == categoryId).FirstOrDefault();
                    if (productGroup != null)
                    {
                        ViewBag.CategoryName = productGroup.CategoryName;
                    }

                }
                return View(pVM);
            }
            else
            {
                using (Db db = new Db())
                {
                    
                    pVM = db.product.ToArray().OrderBy(x => x.Name).Select(x => new ProductViewModel(x)).ToList();
                    
                    ViewBag.CategoryName = "Все товары";

                }
                return View(pVM);
            }
        }
        [HttpGet]
        //[HttpPost]
        [ActionName("product-details")]
        public ActionResult Details(string body)
        {
            ProductViewModel productVM;
            Products productM;
            int id = 0;
            using (Db db = new Db())
            {
                if (!db.product.Any(x => x.Slug.Equals(body)))
                {
                    return RedirectToAction("Category", "Buy");

                }
                //else
                //{
                productM = db.product.Where(x => x.Slug == body).FirstOrDefault();
                id = productM.Id;
                //}
                
                productVM = new ProductViewModel(productM);
                //Get gallery
                productVM.MyPictures = Directory.EnumerateFiles(Server.MapPath("~/Content/images/Upload/Products/" + id + "/Gallery/Thumbs"))
                    .Select(y => Path.GetFileName(y));
            }
            
           
            return View("Details", productVM);
        }
        [HttpGet]
        [ActionName("allproducts")]
        public ActionResult AllProducts()
        {
            List<ProductViewModel> productVM;
            
            using (Db db = new Db())
            {

                productVM = db.product.ToArray().OrderBy(x => x.Name).Select(x => new ProductViewModel(x)).ToList();
            }

            return View(productVM);
        }
        [HttpGet]
        public ActionResult ClientReviewPartial(int id)
        {
            List<ReviewsViewModel> reviewVM;
            using (Db db = new Db())
            {

                reviewVM = db.reviews.ToArray().OrderBy(x => x.DatePost).Where(x => x.ProductId == id && x.AdminConfirm == true).Select(x => new ReviewsViewModel(x)).ToList();
            }
            return PartialView(reviewVM);
        }





    }
}