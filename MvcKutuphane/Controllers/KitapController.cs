using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var kitaplar = db.TblKitap.ToList();
            return View(kitaplar);
        }
        // Dropboxdan seçim yapılacağı için, veritabanından kategorilerin dropbox içerisine çekilmesi gerekli
        // Bu yüzden HttpGet metodunun içi dolu.
        [HttpGet]
        public ActionResult KitapEkle()
        {
            // KATEGORİ İÇİN
            // Listeden öge seçileceği için List<SelectListItem> ifadesi kullanılıyor.
            // LINQ ifade ile dropbox içerisine gelecek kategori değerleri belirlenir.
            List<SelectListItem> deger1 = (from i in db.TblKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            // dgr1 değerine deger1 değerini taşı.
            // dgr1 değeri View tarafında çağırılacak.
            ViewBag.dgr1 = deger1;

            // YAZAR İÇİN
            List<SelectListItem> deger2 = (from y in db.TblYazar.ToList()
                                         select new SelectListItem
                                         {
                                             Text = y.AD + ' ' + y.SOYAD,
                                             Value = y.ID.ToString()
                                         }).ToList();
            ViewBag.dgr2 = deger2;

            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TblKitap ktp)
        {
            // kategori ve yazar değerleri veritabanında ID olarak tutulduğu için ekleme işlemi yapılırken seçilen kategori
            // ve yazarın IDlerine erişilip, tabloya bu IDler eklenmelidir. İsimler değil.
            var ktg = db.TblKategori.Where(k => k.ID == ktp.TblKategori.ID).FirstOrDefault();
            var yzr = db.TblYazar.Where(y => y.ID == ktp.TblYazar.ID).FirstOrDefault();
            ktp.TblKategori = ktg;
            ktp.TblYazar = yzr; 
            db.TblKitap.Add(ktp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var k = db.TblKitap.Find(id);
            db.TblKitap.Remove(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            var tbl = db.TblKitap.Find(id);
            return View("KitapGetir", tbl); // ktg'den gelen değerle KategoriGetir View'ını döndür
        }
        
    }
}