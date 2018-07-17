using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCar.Controllers
{
    public class ManageUserController : Controller
    {
        Models.ShoppingCarEntities db = new Models.ShoppingCarEntities();

        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];

            //抓取User中的資料,並且放入Models.ManagerUser模型中
            var result = (from s in db.Users
                          select new Models.ManageUser
                          {
                              Id = s.Id,
                              UserName = s.UserName,
                              Email = s.Email
                          }).ToList();

            return View(result);
        }


        // GET: ManageUser/Edit/5
        public ActionResult Edit(string id)
        {
            var result = (from s in db.Users
                          where s.Id == id
                          select new Models.ManageUser
                          {
                              Id = s.Id,
                              UserName = s.UserName,
                              Email = s.Email
                          }).FirstOrDefault();
            if (result != default(Models.ManageUser))
            {
                return View(result);
            }
            else
            {
                TempData["ResultMessage"] = string.Format("使用者[{0}]不存在,請重新操作", id);
                return RedirectToAction("Index");
            }
        }

        // POST: ManageUser/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {

            // TODO: Add update logic here
            var result = (from s in db.Users where s.Id == postback.Id select s).FirstOrDefault();
            if (result != default(Models.User))
            {
                result.UserName = postback.UserName;
                result.Email = postback.Email;
                db.SaveChanges();
                //設定編輯成功訊息
                TempData["ResultMessage"] = string.Format("使用者[{0}]編輯成功",postback.UserName);
                return RedirectToAction("Index");
            }
            //設定編輯錯誤訊息
            TempData["ResultMessage"] = string.Format("使用者[{0}]不存在,請重新操作", postback.UserName);
            return RedirectToAction("Index");
                        
        }
          
    }
}
