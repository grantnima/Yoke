using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;
using Microsoft.AspNetCore.Http;
using YoKe.Infrastructure;

namespace FlowerWorld.Controllers
{
    public class CartController : Controller
    {
        private readonly YoKeDB_dataContext db;
        public CartController(YoKeDB_dataContext _db)
        {
            db = _db;
        }
        //
        // GET: /Cart/
        public ActionResult Index()
        {

            if (Request.Query["retUrl"].ToString() != "")
            {
                ViewBag.continueBuy = Request.Query["retUrl"].ToString();
            }
            else
            {
                ViewBag.continueBuy = Request.Headers["Referer"].ToString();
            }
            ViewBag.contBuy = ViewBag.continueBuy;

            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            List<int[]> curOrder = HttpContext.Session.GetJson<List<int[]>>("Order");
            if (curCart == null) curCart = new List<int[]>();
            if (curFavi == null) curFavi = new List<int[]>();
            if (curOrder == null) curOrder = new List<int[]>();
            List<CartItem> cart = new List<CartItem>();
            List<CartItem> favi = new List<CartItem>();
            List<CartItem> order = new List<CartItem>();
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
                                             id = p.ObjId,
                                             price = (double)p.Price,
                                             qty = curQty,
                                             
                                         }).FirstOrDefault<CartItem>();
                cart.Add(cartItem);
            }
            foreach (int[] i in curFavi)
            {
                int curId = i[0];
                int curQty = i[1];
                CartItem cartItem = (from p in db.Product
                                     where p.ObjId == curId
                                     select
                                         new CartItem
                                         {
                                             productName = p.ProductName,
                                             id = p.ObjId,
                                             price = (double)p.Price,
                                             qty = curQty,
                                             
                                         }).FirstOrDefault<CartItem>();
                favi.Add(cartItem);
            }
            foreach (int[] i in curOrder)
            {
                int curId = i[0];
                int curQty = i[1];
                CartItem cartItem = (from p in db.Product
                                     where p.ObjId == curId
                                     select
                                         new CartItem
                                         {
                                             productName = p.ProductName,
                                             id = p.ObjId,
                                             price = (double)p.Price,
                                             qty = curQty,

                                         }).FirstOrDefault<CartItem>();
                order.Add(cartItem);
            }
            ViewBag.cart = cart;
            ViewBag.favi = favi;
            ViewBag.order = order;
            return View("Cart");
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

        public RedirectResult AddFavi(int id)
        {
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            if (curFavi == null)
                HttpContext.Session.SetJson("Favi", new List<int[]> { new int[] { id, 1 } });
            else
            {
                bool found = false;
                foreach (var p in curFavi)
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
                    curFavi.Add(new int[] { id, 1 });
                }
                HttpContext.Session.SetJson("Favi", curFavi);
            }

            string continueBuy = Request.Headers["Referer"].ToString();
            if (Request.Query["retUrl"].ToString() != "")
            {
                continueBuy = Request.Query["retUrl"].ToString();
            }
            return Redirect(continueBuy);
        }
        public ActionResult AddOrder(int id)
        {
            List<int[]> curOrder = HttpContext.Session.GetJson<List<int[]>>("Cart");
            if (curOrder == null)
                HttpContext.Session.SetJson("Cart", new List<int[]> { new int[] { id, 1 } });
            else
            {
                bool found = false;
                foreach (var p in curOrder)
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
                    curOrder.Add(new int[] { id, 1 });
                }
                HttpContext.Session.SetJson("Order", curOrder);
            }
            return Index();
        }

        public RedirectResult updateCartRow(int id)
        {
            int value = int.Parse(Request.Query["value"].ToString());
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            curCart[id][1] = value;
            HttpContext.Session.SetJson("Cart", curCart);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult deleCartRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            curCart.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult storeCartRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            if (curFavi == null)
            {
                curFavi = new List<int[]>();
                HttpContext.Session.SetJson("Favi", curFavi);
            }
            curFavi.Add(curCart[id]);
            curCart.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult deleFaviRow(int id)
        {
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            curFavi.RemoveAt(id);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult buyFaviRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            curCart.Add(curFavi[id]);
            curFavi.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
    }
}
