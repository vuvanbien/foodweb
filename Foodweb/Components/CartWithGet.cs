using Foodweb.Infrastructure;
using Foodweb.Models;

using Microsoft.AspNetCore.Mvc;

namespace Foodweb.Components
{
    public class CartWithGet : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
