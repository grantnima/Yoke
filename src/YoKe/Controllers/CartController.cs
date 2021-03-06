﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;
using Microsoft.AspNetCore.Http;
using YoKe.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Yoke.Controllers
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
        [Authorize]
        public ActionResult Index(int page = 1,int pageSize = 4)
        {
            ProductList pro = new ProductList();
            pro.POrders = new List<PlaceOrder>();
            //var POrders = db.PlaceOrder.Where<PlaceOrder>(m => m.TheCustomer == db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId);
            var porderslist = db.PlaceOrder.Where<PlaceOrder>(m => m.TheCustomer == db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId);
            //var POrders = db.PlaceOrder.Where<PlaceOrder>(m => m.ObjId > 0).Take<PlaceOrder>(12);
            var POrders = porderslist.Skip((page - 1) * pageSize).Take(pageSize);
            var theCustomerId = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId;
            ViewBag.name = User.Identity.Name;
            ViewBag.mobilephone = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).MobilePhone;
            foreach (var p in POrders)
            {
                PlaceOrder po = new PlaceOrder();
                po = new PlaceOrder { ObjId = p.ObjId, Address = p.Address, Brand = p.Brand, Price = p.Price, TheProductName = p.TheProductName, TheCustomer = theCustomerId, Remarks = p.Remarks,BigImg = p.BigImg };
                pro.POrders.Add(po);
            }
            pro.PagingInfoOrd = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = porderslist.Count() };
            //我的发布--商品
            pro.PProducts = new List<Product>();
            var productslist = db.Product.Where<Product>(m => m.TheCustomer == db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId);
            var PProducts = productslist.Skip((page - 1) * pageSize).Take(pageSize);
            foreach (var pp in PProducts)
            {
                Product product = new Product();
                product = new Product { ObjId=pp.ObjId, ProductName = pp.ProductName, Feature = pp.Feature, Price = pp.Price, TheCustomer = theCustomerId,BigImg = pp.BigImg };
                pro.PProducts.Add(product);
            }
            pro.PagingInfoPro = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = productslist.Count() };

            //List<Orders> or = db.Orders.Select<Orders, >;
            //var or =  from m in db.Orders select m;

            //销售订单显示
            var porder_show = (from p in db.PlaceOrder where p.TheCustomer == theCustomerId && p.PalceOrderState == 1
                               select new PlaceOrder
                               {
                                   TheProductName = p.TheProductName,
                                   BigImg = p.BigImg,
                                   ObjId = p.ObjId,
                                   Price = p.Price
                               }
                               );
            var porder_show2 = (from p in db.PlaceOrder
                               where p.TheCustomer == theCustomerId && p.PalceOrderState == 2
                               select new PlaceOrder
                               {
                                   TheProductName = p.TheProductName,
                                   BigImg = p.BigImg,
                                   ObjId = p.ObjId,
                                   Price = p.Price
                               }
                               );


            var order_show = (from p in db.Orders where p.TheCustomer == theCustomerId
                            join w in db.Product on p.TheProduct equals w.ObjId
                            select new SOrder
                            {
                                o_no = p.ObjId,
                                p_name = w.ProductName,
                                price = (double)w.Price,
                                img = (string)w.BigImg
                            });
              
           



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
                                             img = p.BigImg
                                             
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
                                             img = p.BigImg
                                             
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
                                             img = p.BigImg

                                         }).FirstOrDefault<CartItem>();
                order.Add(cartItem);
                //InsertOrder(cartItem);
            }
            ViewBag.cart = cart;
            ViewBag.favi = favi;
            ViewBag.order = order;
            ViewBag.or = order_show;
            ViewBag.po = porder_show;
            ViewBag.po2 = porder_show2;

            return View("Cart",pro);
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

        [HttpPost]
        public ActionResult Upload([FromServices]IHostingEnvironment env, ProductList pl,Product p)
        {
            var fileName = Path.Combine("upload", DateTime.Now.ToString("MMddHHmmss") + ".jpg");
            using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
            {
                pl.PImg.CopyTo(stream);
            }
            Product c = db.Product.Add(new Product()).Entity;
            c.ProductName = p.ProductName;
            c.Feature = p.Feature;
            c.Description = p.Description;
            c.Price = p.Price;
            c.BigImg = fileName;
            c.ProductType = Request.Form["commission"];
            c.TheCustomer = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId;
            db.SaveChanges();
            return Index();
        }
        [HttpPost]
        public ActionResult UploadPlaceOrder([FromServices]IHostingEnvironment env, PlaceOrder po,ProductList pl)
        {
            var fileName = Path.Combine("upload", DateTime.Now.ToString("MMddHHmmss") + ".jpg");
            using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
            {
                pl.OImg.CopyTo(stream);
            }
            PlaceOrder c = db.PlaceOrder.Add(new PlaceOrder()).Entity;
            c.TheCustomer = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId;
            c.Address = po.Address;
            c.Brand = po.Brand;
            c.Price = po.Price;
            c.Producer = po.Producer;
            c.Quantity = po.Quantity;
            c.Remarks = po.Remarks;
            c.TheProductName = po.TheProductName;
            c.BigImg = fileName;
            c.PalceOrderState = 0;
            db.SaveChanges();
            return Index();
        }
        //public ActionResult ShowMyPlaceOrder()
        //{
        //    ProductList pro = new ProductList();
        //    pro.POrders = new List<PlaceOrder>();

        //    ProductList pro = new ProductList();
        //    pro.PlaceOrders = new List<PlacceOrderList>();
        //    //db.Product.Where<Product>(m => m.ObjId > 0).OrderBy<Product, float>(m => (float)m.Price);
        //    var PlaceOrders = db.PlaceOrder.Where<PlaceOrder>(m => m.TheCustomer == db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId);
        //    foreach (var pl in PlaceOrders)
        //    {
        //        pro.PlaceOrders.Add(new PlacceOrderList
        //        {
        //            ProductName = pl.TheProductName,
        //            Price = pl.Price,
        //            Quantity = pl.Quantity,
        //            theCustomer = pl.TheCustomer
        //        });
        //    }
        //    return View();
        //}
        public ActionResult TakeOrder(int id)
        {
            db.PlaceOrder.SingleOrDefault(u => u.ObjId == id).PalceOrderState = 1;
            
            db.SaveChanges();

            return Index();
        }
        public ActionResult UpdatePOrder(int id)
        {
            db.PlaceOrder.SingleOrDefault(u => u.ObjId == id).PalceOrderState = 2;
            db.SaveChanges();
            return Index();
        }

        public ActionResult DelProduct(int id)
        {
            Product p = new Product() { ObjId = id };
            db.Product.Attach(p);
            db.Product.Remove(p);
            db.SaveChanges();
            return Index();
        }
        public ActionResult DelPOrder(int id)
        {
            PlaceOrder p = new PlaceOrder() { ObjId = id };
            db.PlaceOrder.Attach(p);
            db.PlaceOrder.Remove(p);
            db.SaveChanges();
            return Index();
        }
        //public void InsertOrder(CartItem c)
        //{
        //    Orders o = db.Orders.Add(new Orders()).Entity;
        //    o.TheProduct = c.id;

        //}
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
        public RedirectResult deleOrderRow(int id)
        {
            List<int[]> curOrder = HttpContext.Session.GetJson<List<int[]>>("Order");
            curOrder.RemoveAt(id);
            HttpContext.Session.SetJson("Order", curOrder);
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
