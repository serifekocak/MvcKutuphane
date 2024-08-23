using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TblPersonel.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(TblPersonel p)
        {
            // Data annatotions'a bağlı olan geçerlilik sağlanmadıysa PersonelEkle view'ını geri döndür.
            // Sağlandıysa ekleme işlemini yap
            if(!ModelState.IsValid) 
            {
                return View("PersonelEkle");
            }
            db.TblPersonel.Add(p);
            db.SaveChanges();  
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(int id)
        {
            // id'ye göre bul
            var personel = db.TblPersonel.Find(id);
            // Sil
            db.TblPersonel.Remove(personel);
            // kaydet
            db.SaveChanges();
            // Index Action'ına yönlendir (Tekrar listele)
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TblPersonel.Find(id);
            return View("PersonelGetir", prs); // ktg'den gelen değerle PersonelGetir View'ını döndür
        }

        public ActionResult PersonelGuncelle(TblPersonel p)
        {
            var prs = db.TblPersonel.Find(p.ID);
            prs.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}