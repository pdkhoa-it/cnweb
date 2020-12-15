using NoiThat.Models;
using NoiThat.Models.DBIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        public ActionResult Index()
        {
            DBIO db = new DBIO();
            List<PhanQuyen> list = db.GetList_PhanQuyen();

            return View(list);
        }

        [HttpPost]
        public JsonResult AddPhanQuyen(FormCollection Data)
        {
            string ten = Data["tenquyen"];

            JsonResult js = new JsonResult();

            if (string.IsNullOrEmpty(ten))
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Không được bỏ trống dữ liệu"
                };
            }
            else
            {
                DBIO db = new DBIO();
                PhanQuyen p = new PhanQuyen();
                p.Ten = ten;

                db.AddObject(p);
                db.Save();

                int ma = db.Get_ID_PhanQuyen(ten);

                js.Data = new
                {
                    status = "OK",
                    message = ma
                };
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePhanQuyen(FormCollection Data)
        {
            int maquyen = int.Parse(Data["maquyen"]);

            JsonResult js = new JsonResult();

            if (maquyen <= 0)
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Không được bỏ trống dữ liệu"
                };
            }
            else
            {
                DBIO db = new DBIO();
                PhanQuyen p = db.GetObject_PhanQuyen(maquyen);
                if (p == null)
                {
                    js.Data = new
                    {
                        status = "Error",
                        message = "Dữ liệu không tồn tại!"
                    };
                }
                else
                {
                    db.DeleteOBject(p);
                    db.Save();
                    js.Data = new
                    {
                        status = "OK"
                    };
                }
            }

            return Json(js, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditPhanQuyen(FormCollection Data)
        {
            int maquyen = int.Parse(Data["maquyen"]);
            string tenquyen = Data["tenquyen"];

            JsonResult js = new JsonResult();

            if (String.IsNullOrEmpty(tenquyen))
            {
                js.Data = new
                {
                    status = "Error",
                    message = "Không được bỏ trống dữ liệu"
                };
            }
            else
            {
                DBIO db = new DBIO();
                PhanQuyen p = db.GetObject_PhanQuyen(maquyen);
                if (p == null)
                {
                    js.Data = new
                    {
                        status = "Error",
                        message = "Dữ liệu không tồn tại!"
                    };
                }
                else
                {
                    p.Ten = tenquyen;
                    db.Save();

                    js.Data = new
                    {
                        status = "OK"
                    };

                }
            }
            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}