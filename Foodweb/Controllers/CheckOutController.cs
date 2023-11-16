using Foodweb.Infrastructure;
using Foodweb.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Foodweb.Controllers
{
    public class CheckOutController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly FoodContext _context;
        public CheckOutController(FoodContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(string fullname,string sdt, string diachi, string note)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            var userPhone = HttpContext.Session.GetString("UserName");

            if (userPhone == null)
            {

                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                

                
                var cus = new Customer
                {
                    FullName = fullname,
                    Phone = sdt,
                    Address =diachi

                };
                _context.Add(cus);
                _context.SaveChanges();
                int generatedCusId = cus.CusId;
                var computeTotalValue = Convert.ToDouble(HttpContext.Session.GetString("ComputeTotalValue"));
                var order = new Order
                {
                    CusId = generatedCusId,
                    OrderDate = DateTime.Now,
                    TotalMoney = computeTotalValue + 20000,
                    Note = note,
                    Address = diachi


                };
                _context.Add(order);
                _context.SaveChanges();
                List<CartLine> Lines = HttpContext.Session.GetJson<List<CartLine>>("Cart") ?? new List<CartLine>();
                foreach (var cart in Lines)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.OrderId;
                    orderdetail.ProId = cart.Product.ProId;
                    orderdetail.ProName = cart.Product.ProName;
                    orderdetail.Amount = cart.Quantity;
                    orderdetail.TotalMoney = cart.Product.Price * cart.Quantity;
                    orderdetail.CreateDate = DateTime.Now;
                    _context.Add(orderdetail);
                    _context.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Thanh toán thành công, vui lòng chờ duyệt đơn hàng";
                return View("Index",Cart);
            }
            
        }
    }
}
