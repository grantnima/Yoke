using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;
using YoKe.Infrastructure;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Yoke.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace YoKe.Controllers
{
    
    public class OrderController : Controller
    {
        private IHostingEnvironment host = null;
        private readonly YoKeDB_dataContext db;
        public OrderController(YoKeDB_dataContext _db,IHostingEnvironment _host)
        {
            db = _db;
            host = _host;
        }
        public IActionResult Index()
        {
            ViewBag.Request = Request;
            string uid = User.Identity.Name;
            OrderViewModel ovm = new OrderViewModel();
            ovm.orders = new List<OrderInfo>();
            ovm.payment = new Payment();
            ovm.curCustomer = db.Customer.Single(m => m.Email == uid);
            ViewBag.payments = db.PaymentType.Where(m => m.ObjId > 0).ToArray<PaymentType>();
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            ovm.orderQty = 0;
            ovm.payment.Amount = 0.0;
            foreach (var cartItem in curCart)
            {
                ovm.orderQty += cartItem[1];
                int pObjId = cartItem[0];
                for (int i = 0; i < cartItem[1]; i++)
                {
                    var product = db.Product.Single(m => m.ObjId == pObjId);
                    ovm.orders.Add(new OrderInfo { theProduct = product.ObjId, theCustomer = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId, thePayment = 1, productName = product.ProductName});
                    ovm.payment.Amount += db.Product.SingleOrDefault(m => m.ObjId == pObjId).Price;
                }
            }
            return View("Order",ovm);
        }
        [HttpPost]
        public ActionResult Index(OrderViewModel ovm)
        {
            ViewBag.Request = Request;
            //更新客户联系信息
            Customer curCust = db.Customer.Single(m => m.Email == ovm.curCustomer.Email);
            //var manager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ApplicationDbContext()));
            //var c = manager.FindById<ApplicationUser, int>(ovm.curCustomer.objId);
            if (curCust.MobilePhone != ovm.curCustomer.MobilePhone && ovm.curCustomer.MobilePhone != "")
                curCust.MobilePhone = ovm.curCustomer.MobilePhone;
            if (curCust.Email != ovm.curCustomer.Email && ovm.curCustomer.Email != "")
                curCust.Email = ovm.curCustomer.Email;
            //manager.Update(c);
            db.SaveChanges();
            //保存订单。需做事务处理！在.NET EF core中，一个SaveChange方法所提交的内容会自动实现事务处理。
            bool succeed = true;
            int payId = 0;
            try
            {
                //using (TransactionScope ts = new TransactionScope())
                //{
                EntityEntry<Payment> p = db.Payment.Add(new Payment());
                p.Entity.Amount = double.Parse(Request.Form["paymentAmt"]);
                p.Entity.ThePaymentType = int.Parse(Request.Form["paymentType"]);
                p.Entity.PaymentState = 0;
                db.SaveChanges();
                for (int i = 0; i < ovm.orderQty; i++)
                {
                    EntityEntry<Orders> o = db.Orders.Add(new Orders());
                    o.Entity.ThePayment = p.Entity.ThePaymentType;
                    o.Entity.TheCustomer = curCust.ObjId;
                    o.Entity.TheProduct = int.Parse(Request.Form["productId_" + i].ToString().Trim());
                    o.Entity.OrderState = 0;
                    o.Entity.OrderTime = DateTime.Now;
                    o.Entity.PaymentObjId = p.Entity.ObjId;
                    //Orders o = db.Orders.Add(new Orders()).Entity;
                    //o.ThePayment = p.Entity.ObjId;
                    //o.TheCustomer = curCust.ObjId;
                    //o.TheProduct = int.Parse(Request.Form["productId_" + i].ToString().Trim());
                    //o.OrderState = 0;
                    //o.OrderTime = DateTime.Now;
                    //}
                    db.SaveChanges();
                    payId = p.Entity.ObjId;
                    //ts.Complete();
                }
            }
            catch(Exception e)
            {
                succeed = false;
                //Response.WriteAsync(e.ToString());
                throw (e);
            }
            if (succeed)
            {//进入支付处理
                string paymentUrl = "", paymentMethod = "";
                foreach (PaymentType pt in db.PaymentType.Where(m => m.ObjId > 0).ToArray<PaymentType>())
                {
                    if (pt.ObjId == int.Parse(Request.Form["paymentType"]))
                    {
                        paymentUrl = pt.Url;
                        paymentMethod = pt.TypeName;
                        break;
                    }
                }
                //string merchantId = "Flower001";
                //string returnUrl = "http://" + Request.Host + Url.Action("Index", "Payment");
                //string amtStr = Request.Form["paymentAmt"];
                //string merTransId = payId.ToString();
                //根据paymentMethod产生提交付款的对象并提交。以下为暂时的写法，无扩展性。
                //理想的写法是定义付款接口，针对不同的付款机构，写一个实现了接口的对应的付款类
                //在这里根据方法名构建付款对象，然后再调用接口方法实现付款。
                //这里写法是固定的，暂时使用。
                //await RemotePost.PaymentPost(HttpContext, paymentUrl, merchantId, returnUrl, Request.Form["paymentType"], amtStr, merTransId);
                PayRequestInfo pri = new PayRequestInfo();
                pri.Amt = Request.Form["paymentAmt"];
                pri.MerId = "Team04";
                pri.MerTransId = payId.ToString();
                pri.PaymentTypeObjId = Request.Form["paymentType"];
                pri.PostUrl = paymentUrl;
                pri.ReturnUrl = "http://" + Request.Host + Url.Action("Index", "Payment");
                pri.CheckValue = RemotePost.getCheckValue(host.WebRootPath,pri.MerId, pri.ReturnUrl, pri.PaymentTypeObjId, pri.Amt,pri.MerTransId);
                return View("PayRequest", pri);
            }
            else
            {
                //如果未能成功保存数据则执行以下行。由于ovm中未能将原来的order等数据带回，这里要重新获取
                ovm.orders = new List<OrderInfo>();
                ovm.payment = new Payment();
                //获取信息以显示在页面
                ViewBag.payments = db.PaymentType.Where(m => m.ObjId > 0).ToArray<PaymentType>();
                List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
                ovm.orderQty = 0;
                ovm.payment.Amount = 0.0;
                foreach (var cartItem in curCart)
                {
                    ovm.orderQty += cartItem[1];
                    int pObjId = cartItem[0];
                    for (int i = 0; i < cartItem[1]; i++)
                    {
                        var product = db.Product.Single(m => m.ObjId == pObjId);
                        ovm.orders.Add(new OrderInfo { theProduct = product.ObjId, theCustomer = db.Customer.SingleOrDefault(u => u.Email == User.Identity.Name).ObjId, thePayment = 1, productName = product.ProductName });
                        ovm.payment.Amount += db.Product.SingleOrDefault(m => m.ObjId == pObjId).Price;
                    }
                }
                return View("Order", ovm);
            }
        }
    }
}