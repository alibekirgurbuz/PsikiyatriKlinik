using Microsoft.AspNetCore.Mvc;
using PsikiyatriKlinik.Data;
using PsikiyatriKlinik.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace PsikiyatriKlinik.Controllers
{
    public class HastaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HastaController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void SetHastaAdToViewBag()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var hasta = _context.Hastalar.FirstOrDefault(h => h.Id == userId);
                if (hasta != null)
                    ViewBag.Ad = hasta.Ad + " " + hasta.Soyad;
            }
        }

        // GET: Hasta/KisiselBilgilerim
        public IActionResult KisiselBilgilerim()
        {
            SetHastaAdToViewBag();
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var user = _context.Hastalar.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = new ProfileViewModel
            {
                Id = user.Id,
                Ad = user.Ad,
                Soyad = user.Soyad,
                TcKimlikNo = user.TcKimlikNo,
                Telefon = user.Telefon,
                Email = user.Email,
                DogumTarihi = user.DogumTarihi,
                Adres = user.Adres
            };

            return View(model);
        }

        // POST: Hasta/KisiselBilgilerim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KisiselBilgilerim(ProfileViewModel model)
        {
            SetHastaAdToViewBag();
            if (ModelState.IsValid)
            {
                var user = _context.Hastalar.FirstOrDefault(u => u.Id == model.Id);

                if (user != null)
                {
                    user.Ad = model.Ad;
                    user.Soyad = model.Soyad;
                    user.TcKimlikNo = model.TcKimlikNo;
                    user.Telefon = model.Telefon;
                    user.Email = model.Email;
                    user.DogumTarihi = model.DogumTarihi;                   
                    user.Adres = model.Adres;

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Bilgiler başarıyla güncellendi.";
                }
            }

            return RedirectToAction("KisiselBilgilerim");
        }
        // GET: Hasta/HastaScreen
        public IActionResult HastaScreen()
        {
            SetHastaAdToViewBag();
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var user = _context.Hastalar.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = user.Ad;
            ViewBag.UserEmail = user.Email;

            return View();
        }
        [HttpGet]
        public IActionResult Randevularim()
        {
            SetHastaAdToViewBag();
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var doktorlar = _context.Doktorlar.ToList();

            var randevular = _context.Randevular
                .Where(r => r.HastaId == userId)
                .Select(r => new RandevuViewModel
                {
                    Id = r.Id,
                    RandevuTarihi = r.RandevuTarihi,
                    RandevuSaati = r.RandevuSaati,
                    DoktorAdi = r.DoktorAdi,
                    RandevuDurumu = r.RandevuDurumu
                })
                .ToList();

            ViewBag.Doktorlar = doktorlar;
            ViewBag.Randevular = randevular;

            return View(new RandevuViewModel());
        }

        [HttpPost]
        public IActionResult RandevuOlusturGuncelle(RandevuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Formda eksik veya hatalı alanlar var: " + string.Join(", ", errors);
                return RedirectToAction("Randevularim");
            }

            // 🔐 Sadece giriş yapan hastanın UserId'si
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            // Kullanıcının veritabanında olup olmadığını kontrol et
            var hasta = _context.Hastalar.FirstOrDefault(h => h.Id == userId);
            if (hasta == null)
            {
                // Kullanıcı bulunamazsa, oturumu temizle ve giriş sayfasına yönlendir
                HttpContext.Session.Clear();
                TempData["ErrorMessage"] = "Kullanıcı bilgilerinize ulaşılamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            // Doktor bilgisi getir
            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == model.DoktorId);
            if (doktor == null)
            {
                TempData["ErrorMessage"] = "Geçersiz doktor seçimi.";
                return RedirectToAction("Randevularim");
            }

            if (model.Id == 0)
            {
                // Randevu ekle
                var yeniRandevu = new Randevu
                {
                    HastaId = userId, // ✅ formdan gelmedi, session'dan geldi
                    DoktorId = model.DoktorId,
                    DoktorAdi = doktor.Ad + " " + doktor.Soyad,
                    RandevuTarihi = model.RandevuTarihi,
                    RandevuSaati = model.RandevuSaati,
                    RandevuDurumu = model.RandevuDurumu
                };

                _context.Randevular.Add(yeniRandevu);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Randevu oluşturuldu.";
            }
            else
            {
                // Güncelleme işlemi
                var randevu = _context.Randevular.FirstOrDefault(r => r.Id == model.Id && r.HastaId == userId);
                if (randevu != null)
                {
                    randevu.DoktorId = model.DoktorId;
                    randevu.DoktorAdi = doktor.Ad + " " + doktor.Soyad;
                    randevu.RandevuTarihi = model.RandevuTarihi;
                    randevu.RandevuSaati = model.RandevuSaati;
                    randevu.RandevuDurumu = model.RandevuDurumu;

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Randevu güncellendi.";
                }
            }

            return RedirectToAction("Randevularim");
        }

        [HttpPost]
        public IActionResult RandevuSil(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                _context.SaveChanges();
            }
            return RedirectToAction("Randevularim");
        }

        [HttpGet]
        public IActionResult GetRandevu(int id)
        {
            var randevu = _context.Randevular
                .Where(r => r.Id == id)
                .Select(r => new
                {
                    id = r.Id,
                    randevuTarihi = r.RandevuTarihi,
                    randevuSaati = r.RandevuSaati,
                    doktorId = r.DoktorId,
                    randevuDurumu = r.RandevuDurumu
                })
                .FirstOrDefault();

            if (randevu == null)
            {
                return NotFound();
            }

            return Json(randevu);
        }

        public IActionResult Hastalar()
        {
            var hastalar = _context.Hastalar
                .Where(h => h.KullaniciTipi == "Hasta")
                .Select(h => new
                {
                    h.Id,
                    h.Ad,
                    h.Soyad,
                    Yas = h.DogumTarihi.HasValue ? DateTime.Now.Year - h.DogumTarihi.Value.Year : 0,
                    h.Cinsiyet
                })
                .ToList();

            ViewBag.Hastalar = hastalar;
            return View();
        }
    }
}
