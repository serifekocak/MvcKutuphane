using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblYazar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TblYazar y)
        {
            db.TblYazar.Add(y);
            db.SaveChanges();
            return View();
        }
        public ActionResult YazarSil(int id)
        {
            var y = db.TblYazar.Find(id);
            db.TblYazar.Remove(y);
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TblYazar.Find(id);
            return View("YazarGetir", yzr); // ktg'den gelen değerle KategoriGetir View'ını döndür
        }
        public ActionResult YazarGuncelle(TblYazar y)
        {
            var yzr = db.TblYazar.Find(y.ID);
            yzr.AD = y.AD;
            yzr.SOYAD = y.SOYAD;
            yzr.DETAY = y.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}