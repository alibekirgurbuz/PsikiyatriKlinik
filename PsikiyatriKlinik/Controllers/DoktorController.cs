using Microsoft.AspNetCore.Mvc;
using PsikiyatriKlinik.Data;
using PsikiyatriKlinik.Models;
using System.Linq;
using System;

namespace PsikiyatriKlinik.Controllers
{
    public class DoktorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DoktorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult DoctorScreen()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
            return View();
        }
        public IActionResult istatistikler()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
            return View();
        }
        public IActionResult TerapiGecmisi()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
            return View();
        }
        public IActionResult GelenKutusu()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
            return View();
        }
        public IActionResult Hastalarım()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";

            // Önce ilgili randevuları çek
            var randevular = _context.Randevular
                .Where(r => r.DoktorId == userId)
                .ToList();

            // Sonra gruplama ve join işlemlerini bellekte yap
            var hastaListesi = randevular
                .GroupBy(r => r.HastaId)
                .Select(g => g.OrderByDescending(r => r.RandevuTarihi).FirstOrDefault())
                .Join(_context.Hastalar.ToList(),
                      r => r.HastaId,
                      h => h.Id,
                      (r, h) => new
                      {
                          HastaId = h.Id,
                          Ad = h.Ad,
                          Soyad = h.Soyad,
                          DogumTarihi = h.DogumTarihi,
                          Cinsiyet = h.Cinsiyet,
                          SonRandevu = r.RandevuTarihi,
                          SonRandevuSaati = r.RandevuSaati,
                          Durum = r.RandevuDurumu,
                          Telefon = h.Telefon,
                          Email = h.Email
                      })
                .ToList();

            ViewBag.Hastalar = hastaListesi;
            return View();
        }
        public IActionResult HastaProfilleri()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";

            var randevular = _context.Randevular
                .Where(r => r.DoktorId == userId)
                .ToList();

            var hastaProfilleri = randevular
                .GroupBy(r => r.HastaId)
                .Select(g => g.OrderByDescending(r => r.RandevuTarihi).FirstOrDefault())
                .Join(_context.Hastalar.ToList(),
                      r => r.HastaId,
                      h => h.Id,
                      (r, h) => new
                      {
                          HastaId = h.Id,
                          Ad = h.Ad,
                          Soyad = h.Soyad,
                          DogumTarihi = h.DogumTarihi,
                          Cinsiyet = h.Cinsiyet,
                          SonRandevu = r.RandevuTarihi,
                          Durum = r.RandevuDurumu
                      })
                .ToList();

            ViewBag.HastaProfilleri = hastaProfilleri;
            return View();
        }
        public IActionResult GecmisRandevular()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
            return View();
        }
        public IActionResult RandevulariDuzenle()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";

            var randevular = _context.Randevular
                .Where(r => r.DoktorId == userId)
                .OrderByDescending(r => r.RandevuTarihi)
                .ThenByDescending(r => r.RandevuSaati)
                .Join(_context.Hastalar,
                      r => r.HastaId,
                      h => h.Id,
                      (r, h) => new
                      {
                          RandevuId = r.Id,
                          Tarih = r.RandevuTarihi,
                          Saat = r.RandevuSaati,
                          HastaAdSoyad = h.Ad + " " + h.Soyad,
                          Durum = r.RandevuDurumu
                      })
                .ToList();

            ViewBag.Randevular = randevular;
            return View();
        }

        [HttpPost]
        public IActionResult RandevuDurumGuncelle(int id, string yeniDurum)
        {
            var randevu = _context.Randevular.FirstOrDefault(r => r.Id == id);
            if (randevu != null)
            {
                randevu.RandevuDurumu = yeniDurum;
                _context.SaveChanges();
            }
            return RedirectToAction("RandevulariDuzenle");
        }

        public IActionResult Profil()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
            if (doktor != null)
            {
                ViewBag.DoktorAdi = $"Dr. {doktor.Ad} {doktor.Soyad}";
                
                var viewModel = new DoktorProfileViewModel
                {
                    Id = doktor.Id,
                    Ad = doktor.Ad,
                    Soyad = doktor.Soyad,
                    Email = doktor.Email,
                    Telefon = doktor.Telefon,
                    UzmanlikAlani = doktor.UzmanlikAlani,
                    SicilNo = doktor.SicilNo,
                    KlinikAd = doktor.KlinikAd,
                    Adres = doktor.Adres
                };
                
                return View(viewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult Profil(DoktorProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == userId);
                    
                    if (doktor != null)
                    {
                        // Mevcut değerleri koru
                        var mevcutSifre = doktor.Sifre;
                        var mevcutKullaniciTipi = doktor.KullaniciTipi;
                        var mevcutKayitTarihi = doktor.KayitTarihi;
                        var mevcutCinsiyet = doktor.Cinsiyet;

                        // Yeni değerleri ata
                        doktor.Ad = model.Ad;
                        doktor.Soyad = model.Soyad;
                        doktor.Email = model.Email;
                        doktor.Telefon = model.Telefon;
                        doktor.UzmanlikAlani = model.UzmanlikAlani;
                        doktor.SicilNo = model.SicilNo;
                        doktor.KlinikAd = model.KlinikAd;
                        doktor.Adres = model.Adres;

                        // Mevcut değerleri geri yükle
                        doktor.Sifre = mevcutSifre;
                        doktor.KullaniciTipi = mevcutKullaniciTipi;
                        doktor.KayitTarihi = mevcutKayitTarihi;
                        doktor.Cinsiyet = mevcutCinsiyet;

                        _context.Doktorlar.Update(doktor);
                        var result = _context.SaveChanges();

                        if (result > 0)
                        {
                            TempData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi.";
                            return RedirectToAction("Profil");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Güncelleme işlemi başarısız oldu.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                }
            }

            // Hata durumunda mevcut doktor bilgilerini yükle
            int currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var currentDoktor = _context.Doktorlar.FirstOrDefault(d => d.Id == currentUserId);
            if (currentDoktor != null)
            {
                ViewBag.DoktorAdi = $"Dr. {currentDoktor.Ad} {currentDoktor.Soyad}";
            }
            return View(model);
        }
    }
}
