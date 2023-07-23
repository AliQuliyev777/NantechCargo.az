using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NantechCargo.az.Models;
using NantechCargo.az.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace NantechCargo.az.Controllers
{

    public class UserController : Controller
    {

        private readonly CargoContext _sql;

        public UserController(CargoContext sql)
        {
            _sql = sql;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginRegistration(User user)
        {
            user.UserStatusId = 3;
            _sql.Users.Add(user);
            _sql.SaveChanges();
            return View();
            //return RedirectToAction("User", "LoginRegistration");
        }
        public IActionResult LoginPasswordRecovery()
        {
            return View();
        }

        public IActionResult LoginSignIn()
        {
            return View();
        }
        //new Claim(ClaimTypes.Role,User.UserStatusId) Client
        //.Include(x=>x.UserStatus)
        [HttpPost]
        public IActionResult LoginSignIn(User u)
        {
            var User = _sql.Users.SingleOrDefault(x => x.UserFirstName == u.UserFirstName && x.UserPassword == u.UserPassword);

            if (User != null)
            {
                List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, User.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier,User.UserFirstName),
            new Claim(ClaimTypes.Surname,User.UserLastName),
            new Claim(ClaimTypes.Role,User.UserStatusId.ToString())
        };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var prins = new ClaimsPrincipal(claimsIdentity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prins, props);

                if (User.UserStatusId == 3)
                {
                    return RedirectToAction("DashboardHome", "User");
                }
                else if (User.UserStatusId == 1)
                {
                    return RedirectToAction("AdminPanelBlock", "User");
                }
                else if (User.UserStatusId == 2)
                {
                    return RedirectToAction("Orders", "Operator");
                }
            }

            return RedirectToAction("DashboardHome", "User"); // Hata durumunda veya kullanıcı bulunamadığında varsayılan yönlendirme
        }


        [Authorize(Roles = "3")]

        public IActionResult DashboardHome()
        {

            return View();
        }

        //public IActionResult Sifarislerim(int id, User user)
        //{
        //    ViewBag.Status = new SelectList(_sql.Products.ToList(), "ProductId", "ProductOrderId");

        //    using (var item = new CargoContext())
        //    {
        //        var users = item.Orders.Where(x => x.OrderClientId == int.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();

        //        return View(users);
        //    }
        //}

        public IActionResult Sifarislerim()
        {
            var q = _sql.Orders
                .Include(x => x.Products)
                .Include(x => x.OrderClient)
                .Include(x => x.OrderLevel)
                .Where(x => x.OrderClientId == int.Parse(User.FindFirstValue(ClaimTypes.Sid)))
                .Select(x => new OpOrder
                {
                    ClientFullName = x.OrderClient.UserFirstName + " " + x.OrderClient.UserLastName,
                    OrderDate = x.OrderDate.ToString("dd.MM.yyyy hh:mm"),
                    ProductCount = x.Products.Count,
                    OrderId = x.OrderId,
                    ProductNames = x.Products.Select(p => p.ProductName).ToList(),
                    ProductUrl = x.Products.Select(p => p.ProductUrl).ToList(),
                    LevelName = x.OrderLevel.LevelName
                });

            return View(q.ToList());
        }





        [Authorize(Roles = "3")]


        public IActionResult SexsiMelumatlar(int id)
        {

            ViewBag.user = _sql.Users.ToList();
            var a = _sql.Users.SingleOrDefault(x => x.UserId == id);
            return View(a);
        }



        [HttpPost]
        public IActionResult SexsiMelumatlar(int id, User user, IFormFile UserPhoto)
        {
            var KohneMelumat = _sql.Users.SingleOrDefault(x => x.UserId == id);

            if (KohneMelumat == null)
            {
                // Kullanıcı bulunamadı, uygun bir hata işleme mekanizması uygulanabilir.
                return NotFound();
            }

            // Eski fotoğrafı silmek için önceki dosya yolunu al
            string eskiFotoYolu = KohneMelumat.UserPhoto;

            if (UserPhoto != null)
            {
                // Yeni bir fotoğraf yüklendiği varsa, bu fotoğrafı kaydet
                string yeniFotoAdi = Guid.NewGuid().ToString() + Path.GetExtension(UserPhoto.FileName);
                string yeniFotoYolu = Path.Combine("wwwroot/Img", yeniFotoAdi);

                using (var stream = new FileStream(yeniFotoYolu, FileMode.Create))
                {
                    UserPhoto.CopyTo(stream);
                }

                // Kullanıcının fotoğraf yolunu güncelle
                KohneMelumat.UserPhoto = yeniFotoAdi;
            }

            // Kullanıcının diğer bilgilerini güncelle
            KohneMelumat.UserFirstName = user.UserFirstName;
            KohneMelumat.UserLastName = user.UserLastName;
            KohneMelumat.UserBirthDay = user.UserBirthDay;
            KohneMelumat.UserEmail = user.UserEmail;
            KohneMelumat.UserAddress = user.UserAddress;
            KohneMelumat.UserFin = user.UserFin;

            _sql.SaveChanges();

            // Eski fotoğrafı sil
            if (!string.IsNullOrEmpty(eskiFotoYolu))
            {
                string eskiFotoDosyaYolu = Path.Combine("wwwroot/Img", eskiFotoYolu);
                if (System.IO.File.Exists(eskiFotoDosyaYolu))
                {
                    System.IO.File.Delete(eskiFotoDosyaYolu);
                }
            }

            return RedirectToAction("SexsiMelumatlar", new { id = id });
        }

        public IActionResult UserId_ChangePassword(User user, int id)
        {

            return View();
        }
        [HttpPost]
        public IActionResult UserId_ChangePassword(UpdatePass u)
        {
            if (ModelState.IsValid)
            {
                if (u.NewPassword == u.RePassword)
                {
                    User user = _sql.Users.SingleOrDefault(x => x.UserId == int.Parse(User.FindFirstValue(ClaimTypes.Sid)));
                    if (user.UserPassword == u.OldPassword)
                    {
                        user.UserPassword = u.NewPassword;
                        _sql.SaveChanges();
                        return RedirectToAction("DashboardHome", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Kohne sifre yanlisdir");
                    }

                }
                else
                {
                    ModelState.AddModelError("Error", "Tekrar sifre yanlisdir");
                }
            }
            return View();
        }



        public IActionResult SifarislerimAddOrder()
        {
            return View();

        }

        //public IActionResult AdminSifarisTesdiq()
        //{
        //    using (var item = new CargoContext())
        //    {

        //    return View(item.Products.ToList());
        //    }
        //}
   
        [HttpPost]
        public IActionResult SifarislerimAddOrder(Product product)
        {
            _sql.Products.Add(product);
            _sql.SaveChanges();
            return View();
        }


        public IActionResult Baglamalarizleme( int id)
        {
            using (var item = new CargoContext())
            {
                var UserOrder = item.Orders.Include(x => x.Products).Where(x => x.OrderClientId == int.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();
                return View(UserOrder);
            }
        }
      

        [Authorize(Policy = "23")]
        public IActionResult UserID()
        {
            return View();

        }

		public IActionResult UserNotification(int id, Notification notification)
		{
			ViewBag.notification = new SelectList(_sql.Notifications.ToList(), "NotificationId", "NotificationTitle");
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));

			using (var context = new CargoContext())
			{
				var notifications = context.Notifications
					.Where(n => n.NotificationClientId == userId)
					.Include(n => n.NotificationOrder)
						.ThenInclude(o => o.Products)
					.ToList();

				return View(notifications);
			}
		}


		public IActionResult AdminPanelBlock(int id)
        {

            ViewBag.Status = new SelectList(_sql.Users.ToList(), "UserStatusId", "UserStatus");

            using (var item = new CargoContext())
            {
                var users = item.Users.Include(u => u.UserStatus).ToList();

                return View(users);
            }

        }


public IActionResult Cixis()
        {

            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("LoginSignIn", "User");

        }
        public IActionResult Delete(int id)
        {
            var a = _sql.Users.SingleOrDefault(x => x.UserId == id);
            _sql.Users.Remove(a);
            _sql.SaveChanges();
            return RedirectToAction("LoginSignIn", "User");
        }



        public IActionResult NewOrder(int id)
        {

            ViewBag.Branch = new SelectList(_sql.Branchs.ToList(), "BranchId", "BranchName");

            return View();
        }



        [HttpPost]
        public IActionResult AddOrder(int FilialId, string[] ProductName, string[] ProductUrl, decimal[] ProductPrice, decimal[] ProductCargoAmount, int[] ProductCount, Order order)
        {
            Order o = new Order();
            o.OrderBranchId = FilialId;
            o.OrderClientId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            o.OrderLevelId = 1;
            _sql.Orders.Add(o);
            _sql.SaveChanges();

            for (int i = 0; i < ProductName.Length; i++)
            {
                Product p = new Product();
                p.ProductName = ProductName[i];
                p.ProductCargoAmount = ProductCargoAmount[i];
                p.ProductUrl = ProductUrl[i];
                p.ProductPrice = ProductPrice[i];
                p.ProductCount = ProductCount[i];
                p.ProductOrderId = o.OrderId;
                //....
                _sql.Products.Add(p);
                _sql.SaveChanges();
            }
            return RedirectToAction("Sifarislerim", "User");
        }
    }
}
