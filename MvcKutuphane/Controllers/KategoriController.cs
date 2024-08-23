using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        //Modele ait nesne oluştur. Bu nesne kütüphane entities içinde tutulan tabloları ve propertileri tutar.
        DBKutuphaneEntities db = new DBKutuphaneEntities();

        // ActionResult --> Controller yapısına gelen isteklere göre işlem yapıp kullanıcıya View ile isteğe göre bilgileri geri döndüren metotlara verilen isimdir.
        // ActionResult, kullanıcı ile veri iletişimini sağladığı için kullanıcıya gönderilecek veri sayfası Controller ismiyle aynı olan klasörün içerisinde metot ismi ile aynı isimde sayfa olarak bulunmalıdır.

        public ActionResult Index()
        {   
            // Listele
            var degerler = db.TblKategori.ToList();
            // Döndür
            return View(degerler);
        }

        // Sayfa yüklendiğinde çalışsın. (Eğer bu ActionResult olmazsa sayfa her yüklendiğinde boş değer eklemesi yapılır)
        [HttpGet]
        public ActionResult KategoriEkle() 
        {
            return View();
        }

        // Sayfaya gönderme işlemi gerçekleştiğinde çalışsın.
        // Metottaki "TblKategori k" paramtresi formun post edilmesiyle elde ediliyor.
        [HttpPost]
        public ActionResult KategoriEkle(TblKategori k)
        {
            db.TblKategori.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // RedirectToAction --> farklı bir action methoda yönlendirir.
        // // Metottaki "int id" paramtresi    "<a href="/Kategori/KategoriSil/@k.ID">"   ifadesinden alınıyor.  
        public ActionResult KategoriSil(int id) 
        {
            // id'ye göre bul
            var kategori = db.TblKategori.Find(id);
            // Sil
            db.TblKategori.Remove(kategori);
            // kaydet
            db.SaveChanges();
            // Index Action'ına yönlendir (Tekrar listele)
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TblKategori.Find(id);
            return View("KategoriGetir", ktg); // ktg'den gelen değerle KategoriGetir View'ını döndür
        }

        public ActionResult KategoriGuncelle(TblKategori k)
        {
            var ktg = db.TblKategori.Find(k.ID);
            ktg.AD = k.AD;
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
        
    }
}