using Foodweb.Models;
using Microsoft.AspNetCore.Mvc;
using Foodweb.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Foodweb.Controllers
{
    public class AccountsController : Controller
    {
        private readonly FoodContext _context;


        public AccountsController(FoodContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Account user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = _context.Accounts
                    .Where(x => x.Phone.Equals(user.Phone) && x.Password.Equals(user.Password))
                    .FirstOrDefault();

                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Phone.ToString());
                    HttpContext.Session.SetString("FullName", u.FullName.ToString());
                    HttpContext.Session.SetInt32("AccountId", u.AccountId);
                    HttpContext.Session.SetInt32("RoleId", u.RoleId ?? 0); // Lưu RoleId vào session

                    ViewData["UserName"] = u.Phone.ToString();

                    if (u.RoleId == 6)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
            }

            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa dữ liệu từ Session
            HttpContext.Session.Clear();

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }


    }
}
