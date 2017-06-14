using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YoKe.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using Yoke.Infrastructure;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace Yoke.Controllers
{
    public class PaymentController : Controller
    {
        private IHostingEnvironment host = null;
        private readonly YoKeDB_dataContext db;
        public PaymentController(YoKeDB_dataContext _db,IHostingEnvironment _host)
        {
            db = _db;
            host = _host;
        }
        //
        // GET: /Payment/

        public ActionResult Index()
        {
            string merId, amt, merTransId, transId, transTime;
            int paymentTypeObjId = int.Parse(Request.Form["paymentTypeObjId"]);
            PaymentType paymentMethod = db.PaymentType.Single(m => m.ObjId == paymentTypeObjId);
            //这里要根据paymentMethod的值构造验证类的实例，然后调用其验证方法。以下写法为暂时的，无扩展性。    
            if (RemotePost.PaymentVerify(Request,host.WebRootPath,  out merId, out amt, out merTransId, out transId, out transTime) && merId == "Team04")
            {
                Payment pay = db.Payment.Single(m => m.ObjId == int.Parse(merTransId));
                Orders[] orders = db.Orders.Where(m => m.PaymentObjId == int.Parse(merTransId)).ToArray<Orders>();
                pay.TransTime = DateTime.Parse(transTime);
                pay.TransNo = transId;
                foreach (Orders or in orders)
                {
                    or.OrderState = 1;
                }
                db.SaveChanges();
                ViewBag.paymentMsg = "付款成功！     付款号：" + merTransId.ToString() + "；   金额：" + amt.ToString() + "元。";//付款成功！显示付款信息作为测试。
            }
            return View();
        }
    }
}
