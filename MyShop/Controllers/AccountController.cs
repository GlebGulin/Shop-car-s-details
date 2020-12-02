using Binding.Models;
using MyShop.Areas.autoadmin.ViewModels.Store;
using NGLayer.Models.Data;
using NGLayer.Models.Data.Orders;
using NGLayer.Models.ViewModels.Order;
using NGLayer.Models.ViewModels.YourAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return Redirect("~/account/enter");
        }
        
        [ActionName("new-account")]
        [HttpGet]
        public ActionResult NewAccount()
        {
            return View("NewAccount");
        }

        [ActionName("new-account")]
        [HttpPost]
        
        public ActionResult NewAccount(UsersViewModel usModel)
        {
            if (!ModelState.IsValid)
            {
                return View("NewAccount", usModel);
            }
            if (!usModel.ConfirmPass.Equals(usModel.Pass))
            {
                ModelState.AddModelError("", "Пароль не совпадает");
                return View("NewAccount", usModel);
            }

            using (Db db = new Db())
            {
                

                Users user = new Users();
                
                if (db.users.Any(x => x.FirstName == usModel.Mail) || db.users.Any(x => x.NickName == usModel.NickName))
                {
                    ModelState.AddModelError(" ", "Пользователь с именем "+usModel.NickName+ " и почтой "+usModel.Mail+" уже существует");
                    usModel.Mail = "";
                    usModel.NickName = "";
                    return View(usModel);
                }


                user.NickName = usModel.NickName;
                user.FirstName = usModel.FirstName;
                user.SurName = usModel.SurName;
                user.Mail = usModel.Mail;
                user.Pass = usModel.Pass;
                

                db.users.Add(user);
                db.SaveChanges();
                int id = user.Id;
                UserRole usRole = new UserRole()
                {
                    UserId = id,
                    RoleId = 2
                };
                db.userRoles.Add(usRole);
                db.SaveChanges();

            }

            TempData["SM"] = "Успешно зарегестрировались";
            return Redirect("~/Account/enter");
        }


        //[ActionName("enter")]
        [HttpGet]
        //[Authorize]
        public ActionResult Enter()
        {
            string username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                return RedirectToAction("your-profile");
            }
            return View("Enter");
        }
        //[ActionName("enter")]
        [HttpPost]
        //[Authorize]
        public ActionResult Enter(EnterViewModel eModel)
        {
           
            if (!ModelState.IsValid)
            {
                return View("Enter", eModel);
            }
            using (Db db = new Db())
            {
                var login = db.users.Where(x => x.Mail.Equals(eModel.Email) && x.Pass.Equals(eModel.Password)).FirstOrDefault();
                //Users currentUser = db.users.Where(x => x.Mail.Equals(eModel.Email)).FirstOrDefault();
                if (login == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                    eModel.Email = String.Empty;
                    eModel.Password = String.Empty;
                    return View("Enter", eModel);
                }
                else
                {
                    

                    FormsAuthentication.SetAuthCookie(eModel.Email, eModel.RememberMe);
                    return Redirect(FormsAuthentication.GetRedirectUrl(eModel.Email, eModel.RememberMe));
                }
            }

            

        }
        
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Session["GlobalUserName"] = null;
            return Redirect("~/account/enter");
        }
        [Authorize]
        public ActionResult UserInfoPartial()
        {
            //use just email!!!!!
            string usermail = User.Identity.Name;
            UserInfoPartialViewModel uModel;
            using (Db db = new Db())
            {
                Users user = db.users.FirstOrDefault(x => x.Mail == usermail);
                uModel = new UserInfoPartialViewModel();
                uModel.FirstName = user.FirstName;
                uModel.LastName = user.SurName;
            }
            return PartialView(uModel);
        }
        [Authorize]
        [HttpGet]
        [ActionName("your-account")]
        public ActionResult YourAccount()
        {
            YourProfileViewModel ypVM;
            string usermail = User.Identity.Name;
            using (Db db = new Db())
            {
                Users user = db.users.FirstOrDefault(x => x.Mail == usermail);
                //    ypVM = new YourProfileViewModel() {
                //        FirstName = user.FirstName,
                //        SurName = user.SurName,
                //        SurName = u.SurName,
                //        Mail = u.Mail,
                //        NickName = u.NickName,
                //        Pass = u.Pass,
                //        PhoneNumber = u.PhoneNumber,
                //        DateRegistration = u.DateRegistration
                //};
                ypVM = new YourProfileViewModel(user);
               
            }
            return View("YourAccount", ypVM);
        }
        [Authorize]
        [HttpPost]
        [ActionName("your-account")]
        public ActionResult YourAccount(YourProfileViewModel ypVM)
        {
            string usermail = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View("YourAccount", ypVM);
            }
            if (!string.IsNullOrWhiteSpace(ypVM.Pass))
            {
                if (!ypVM.ConfirmPass.Equals(ypVM.Pass))
                {
                    ModelState.AddModelError("", "Пароли не совадают");
                    return View("YourAccount", ypVM);
                }

            using (Db db = new Db())
                {
                    if (db.users.Where(x => x.Id != ypVM.Id).Any(x => x.Mail.Equals(ypVM.Mail)))
                    {
                        ModelState.AddModelError("", "Пользователь с таким почтовым ящиком уже зарегестрирован");
                        ypVM.Mail = String.Empty;
                        return View("YourAccount", ypVM);
                    }
                    Users user = db.users.Find(ypVM.Id);
                    user.FirstName = ypVM.FirstName;
                    user.SurName = ypVM.SurName;
                    user.PhoneNumber = ypVM.PhoneNumber;
                    //if mail was chaned
                    user.Mail = ypVM.Mail;
                    user.NickName = ypVM.NickName;
                    if (!string.IsNullOrWhiteSpace(ypVM.Pass))
                    {
                        user.Pass = ypVM.Pass;
                    }


                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(ypVM.Mail, true);
                }
                

            }
            TempData["SuccessUserChange"] = "Изменения успешно внесены";
            //FormsAuthentication.SignOut();
            //return Redirect("~/account/enter");
            return Redirect("~/account/your-account");
        }
        [Authorize]
        public ActionResult MyOrders()
        {
            List<UserOrdersViewModel> userOrdersViewModel = new List<UserOrdersViewModel>();
            using(Db db = new Db())
            {
                List<OrdersViewModel> ordersViewModels = db.orders.Where(x => x.Mail == User.Identity.Name).ToArray().Select(x => new OrdersViewModel(x)).ToList();
                foreach(var order in ordersViewModels)
                {
                    Dictionary<string, int> prodQuantity = new Dictionary<string, int>();
                    decimal totalCoast = 0m;
                    List<OrderDetail> orderDetails = db.orderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                    int NumberOrder = order.OrderId;
                    DateTime orderDate = order.DateOrder;
                    foreach (var orderDetail in orderDetails)
                    {
                        Products product = db.product.Where(x => x.Id == orderDetail.ProductId).FirstOrDefault();
                        decimal price = product.Price;
                        string productName = product.Name;
                        prodQuantity.Add(productName, orderDetail.Quantity);
                        totalCoast += orderDetail.Quantity * price;
                    }
                    userOrdersViewModel.Add(new UserOrdersViewModel()
                    {
                        OrderId = NumberOrder,
                        DateOrder = orderDate,
                        TotalCoast = totalCoast,
                        ProductQuantity = prodQuantity
                    });
                }
               
            }
            return View(userOrdersViewModel);
        }

        

    }
}