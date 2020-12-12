using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<PhanQuyen> list = new List<PhanQuyen>();
            DBIO db = new DBIO();
            list = db.GetDS();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult AddPhanQuyen(FormCollection data)
        {
            string ten = data["tenquyen"];

            JsonResult js = new JsonResult();

            if(string.IsNullOrEmpty(ten))
            {
                js.Data = new
                {
                    status = "ER", message = "Không được bỏ trống"
                };
            }
            else
            {
                DBIO db = new DBIO();
                PhanQuyen p = new PhanQuyen();
                p.Ten = ten;

                int ma = db.ThemPhanQuyen(p);

                js.Data = new
                {
                    status = "OK", message = ma
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePhanQuyen(FormCollection data)
        {
            int ma = int.Parse( data["ma"]);
            JsonResult js = new JsonResult();
            
            if(ma == 0)
            {
                js.Data = new
                {
                    status = "ER",
                    message = "Sai mã"
                };
            }
            else
            {
                DBIO db = new DBIO();

                db.XoaPhanQuyen(ma);

                js.Data = new
                {
                    status = "OK"
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}