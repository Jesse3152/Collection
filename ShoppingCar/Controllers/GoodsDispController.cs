using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.CSharp;

namespace ShoppingCar.Controllers
{
    public class GoodsDispController : Controller
    {
        //使用ShoppinCarEntities類別取得資料庫所有資料
        private Models.ShoppingCarEntities db = new Models.ShoppingCarEntities();


        // GET: GoodsDisp      
        public ActionResult Index()
        {
            //宣告商品取得列表 result
            List<Models.test> result = new List<Models.test>();

            //接收轉導成功的訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];

            //使用LinQ語法抓取資料庫所需table
            result = (from s in db.tests select s).ToList();

            return View(result);
        }
        #region Insert
        public ActionResult Create()
        {
            return View();
        }

        //Insert資料回傳處理
        [HttpPost]//限定只處理POST方法傳入的資料     
        public ActionResult Create(Models.test PostBack)
        {
            //驗證資料
            if (this.ModelState.IsValid)
            {
                //取資料庫所有資料
                using (Models.ShoppingCarEntities db = new Models.ShoppingCarEntities())
                {
                    //將回傳入資料加入test
                    db.tests.Add(PostBack);


                    //儲存異動資料
                    db.SaveChanges();

                    //Insert成功訊息
                    TempData["ResultMessage"] = string.Format("商品[{0}]新增成功", PostBack.Name);

                    //導向GoodDisController/Index
                    return RedirectToAction("Index");
                }
            }

            //Insert失敗
            ViewBag.ResultMessage = "資料有誤，請檢查";

            //頁面停留在Insert
            return View(PostBack);
        }

        #endregion

        #region Edit 判斷ID是否存在
        [HttpPost]
        public ActionResult Edit(int id)
        {
            //使用LinQ語法where 抓取資料庫所需table 
            //使用test.id抓取該筆資料
            var result = (from s in db.tests where s.id == id select s).FirstOrDefault();

            #region 判斷id是否有資料
            if (result != default(Models.test))
            {
                return View(result);
            }
            else
            {
                TempData["resultMessage"] = "資料有誤,請重新操作";
                //返回商品列表
                return RedirectToAction("Index");
            };
            #endregion
        }
        #endregion

        #region Edit 資料回傳處理
   
        public ActionResult Edit(Models.test Postpack)
        {
            //驗証
            if (this.ModelState.IsValid)
            {
                var result = (from s in db.tests where s.id == Postpack.id select s).FirstOrDefault();

                //指定變動值 result原table的值,Postpack變動後的值

                result.Name = Postpack.Name;
                result.DefaultImageId = Postpack.DefaultImageId;
                result.Price = Postpack.Price;
                result.Quantity = Postpack.Quantity;
                result.PublishDate = Postpack.PublishDate;
                result.Status = Postpack.Status;
                result.CategoryId = Postpack.CategoryId;
                result.Description = Postpack.Description;


                //儲存變動
                db.SaveChanges();

                //設定成訊息並導回商品列表Index
                TempData["ResultMessage"] = string.Format("商品[{0}]編輯成功", Postpack.Name);
                return RedirectToAction("Index");
            }
            else return View(Postpack);//資料錯誤導向自己Edit頁面
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = (from s in db.tests where s.id == id select s).FirstOrDefault();
            //判斷ID是否有資料
            if (result != default(Models.test))
            {
                //移除資料
                db.tests.Remove(result);

                //儲存變更
                db.SaveChanges();

                TempData["ResultMessage"] = string.Format("商品[{0}]成功刪除", result.Name);
                return RedirectToAction("Index");
            }
            else //如果沒有資料顯示錯誤導回Index頁面
            {
                TempData["ResultMessage"] = "指定資料不存在,無法刪除,請重新操作";
                return RedirectToAction("Index");
            }

        }
        #endregion
    }
}