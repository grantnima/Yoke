using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;

namespace YoKe.Controllers
{
    public class HomeController : Controller
    {
        private readonly YoKeDB_dataContext db;
        public HomeController(YoKeDB_dataContext yokedb)
        {
            db = yokedb;
        }
        public IActionResult Index()
        {
            HomeIndexViewModel ivm = new HomeIndexViewModel();
            ivm.Products = new List<ProductList>();
            ProductList pp = new ProductList();
            pp.p = new Product();
            var Products = db.Product.Where<Product>(m => m.ObjId > 0).OrderBy<Product, float>(m => (float)m.Price).Take<Product>(4);
            foreach (var p in Products)
            {
                ProductList pl = new ProductList();
                pl.p = new Product { ObjId = p.ObjId, ProductName = p.ProductName, BigImg = p.BigImg, Price = p.Price };
                
                
                
                ivm.Products.Add(pl);
                
            }
            return View(ivm);
        }

        public IActionResult Detail(int id)
        {
            ProductList pl = new ProductList();
            pl.p = db.Product.Single<Product>(m => m.ObjId == id);
            
            return View(pl);
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
