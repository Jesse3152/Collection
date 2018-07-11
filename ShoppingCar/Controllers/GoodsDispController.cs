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

        // GET: GoodsDisp
        public ActionResult Index()
        {
            //宣告商品取得列表 result
            List<Models.good> result = new List<Models.good>();

            //接收轉導成功的訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];

            //使用ShoppinCarEntities類別取得資料庫所有資料
            using (Models.ShoppingCarEntities db = new Models.ShoppingCarEntities())
            {
                //使用LinQ語法抓取資料庫所需table
                result = (from s in db.goods select s).ToList();
            }

            return View(result);
        }
        //Insert
        public ActionResult Create()
        {
            return View();
        }
        //Insert資料回傳處理
        [HttpPost]//限定只處理POST方法傳入的資料

        public ActionResult Create(Models.good PostBack)
        {
            //驗證資料
            if (this.ModelState.IsValid)
            {
                //取資料庫所有資料
                using (Models.ShoppingCarEntities db = new Models.ShoppingCarEntities())
                {
                    //將回傳入資料加入good
                    db.goods.Add(PostBack);

                    try
                    {
                        //儲存異動資料
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }



                    //Insert成功訊息
                    TempData["ResultMessage"] = string.Format("商品[{0}]新增成功", PostBack.商品名稱);

                    //導向GoodDisController/Index
                    return RedirectToAction("Index");
                }
            }

            //Insert失敗
            ViewBag.ResultMessage = "資料有誤，請檢查";

            //頁面停留在Insert
            return View(PostBack);
        }


    }
}