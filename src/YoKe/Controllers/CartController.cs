using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;
using Microsoft.AspNetCore.Http;
using YoKe.Infrastructure;




// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace YoKe.Controllers
{
    public class CartController : Controller
    {
        private readonly YoKeDB_dataContext db;
        public CartController(YoKeDB_dataContext _db)
        {
            db = _db;
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            
            if (curCart == null) curCart = new List<int[]>();
            
            List<CartItem> cart = new List<CartItem>();
            foreach (int[] i in curCart)
            {
                int curId = i[0];
                int curQty = i[1];
                CartItem cartItem = (from p in db.Product
                                     where p.ObjId == curId
                                     select
                                         new CartItem
                                         {
                                             productName = p.ProductName,
                                             
                                             price = (double)p.Price,
                                               
                                             qty = curQty,
                                             
                                         }).FirstOrDefault<CartItem>();
                cart.Add(cartItem);
            }

            ViewBag.cart = cart;
            return View("PersonalCenter");
        }

        public ActionResult AddCart(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            if (curCart == null)
                HttpContext.Session.SetJson("Cart", new List<int[]> { new int[] { id, 1 } });
            else
            {
                bool found = false;
                foreach (var p in curCart)
                {
                    if (p[0] == id)
                    {
                        found = true;
                        p[1] += 1;
                        break;
                    }
                }
                if (!found)
                {
                    curCart.Add(new int[] { id, 1 });
                }
                HttpContext.Session.SetJson("Cart", curCart);
            }


            return Index();
        }
    }
}
