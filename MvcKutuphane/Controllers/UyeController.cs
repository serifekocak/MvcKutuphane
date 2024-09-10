using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        // GET: Uye
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TblUyeler.ToList().ToPagedList(sayfa, 3);   
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeEkle(TblUyeler u)
        {
            // Data annatotions'a bağlı olan geçerlilik sağlanmadıysa PersonelEkle view'ını geri döndür.
            // Sağlandıysa ekleme işlemini yap
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TblUyeler.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}