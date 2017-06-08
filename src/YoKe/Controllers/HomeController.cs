using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;


// 模拟支付平台：  http://10.0.14.129:8801/default.aspx
// 商家代号： Team04
// 

namespace YoKe.Controllers
{
    public class HomeController : Controller
    {
        private readonly YoKeDB_dataContext db;
        public HomeController(YoKeDB_dataContext yokedb)
        {
            db = yokedb;
        }
        public IActionResult Index(int page = 1,int pageSize = 4)
        {
            HomeIndexViewModel ivm = new HomeIndexViewModel();
            ivm.Products = new List<ProductList>();
            ivm.POrders = new List<PlaceOrder>();
            //ProductList pp = new ProductList();
            //pp.p = new Product();
            //var Products = db.Product.Where<Product>(m => m.ObjId > 0).OrderBy<Product, float>(m => (float)m.Price).Take<Product>(12);
            var POrders = db.PlaceOrder.Where<PlaceOrder>(m => m.ObjId > 0).Take<PlaceOrder>(12);
            var productlist = db.Product.Where<Product>(m => m.ObjId > 0).OrderBy<Product, float>(m => (float)m.Price);
            var Products = productlist.Skip((page - 1) * pageSize).Take(pageSize);
            foreach (var o in Products)
            {
                ivm.Products.Add(new ProductList
                {
                    ObjId = o.ObjId,
                    ProductName = o.ProductName,
                    Price = o.Price
                });
                //ProductList pl = new ProductList();
                //pl.p = new Product { ObjId = p.ObjId, ProductName = p.ProductName, BigImg = p.BigImg, Price = p.Price };
                //ivm.Products.Add(pl);

            }
            foreach (var p in POrders)
            {
                PlaceOrder po = new PlaceOrder();
                po = new PlaceOrder { ObjId = p.ObjId, Address = p.Address, Brand = p.Brand, Price = p.Price,TheProductName = p.TheProductName };
                ivm.POrders.Add(po);
            }
            ivm.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = pageSize, TotalItems = productlist.Count() };
            return View(ivm);
        }

        public IActionResult Detail(int id)
        {
            ProductList pl = new ProductList();
            pl.p = db.Product.Single<Product>(m => m.ObjId == id);
            
            return View(pl);
        }
        public IActionResult PDetail(int id)
        {
            PlaceOrder po = new PlaceOrder();
            po = db.PlaceOrder.Single<PlaceOrder>(m => m.ObjId == id);
            return View(po);
        }
        public IActionResult PersonalCenter()
        {
            
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
