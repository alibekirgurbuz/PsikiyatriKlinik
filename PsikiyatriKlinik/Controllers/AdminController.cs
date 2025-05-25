using Microsoft.AspNetCore.Mvc;
using PsikiyatriKlinik.Data;
using System.Linq;
using System;
using PsikiyatriKlinik.Models;

namespace PsikiyatriKlinik.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminScreen()
        {
            return View();
        }
        public IActionResult Istatistikler()
        {
            return View();
        }
        public IActionResult Doktorlar()
        {
            var doktorlar = _context.Doktorlar
                .Select(d => new DoktorListViewModel
                {
                    Id = d.Id,
                    Ad = d.Ad,
                    Soyad = d.Soyad,
                    UzmanlikAlani = d.UzmanlikAlani,
                    HastaSayisi = _context.Randevular.Count(r => r.DoktorId == d.Id)
                })
                .ToList();

            return View(doktorlar);
        }
        public IActionResult Hastalar()
        {
            var hastalar = _context.Hastalar
                .Where(h => h.KullaniciTipi == "Hasta")
                .Select(h => new HastaListViewModel
                {
                    Id = h.Id,
                    Ad = h.Ad,
                    Soyad = h.Soyad,
                    Yas = h.DogumTarihi.HasValue ? DateTime.Now.Year - h.DogumTarihi.Value.Year : 0,
                    Cinsiyet = h.Cinsiyet
                })
                .ToList();

            return View(hastalar);
        }
        [HttpPost]
        public IActionResult HastaSil(int id)
        {
            // Önce hastanın randevularını sil
            var randevular = _context.Randevular.Where(r => r.HastaId == id).ToList();
            if (randevular.Any())
            {
                _context.Randevular.RemoveRange(randevular);
            }

            // Sonra hastayı sil
            var hasta = _context.Hastalar.FirstOrDefault(h => h.Id == id);
            if (hasta != null)
            {
                _context.Hastalar.Remove(hasta);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Hasta ve ilişkili randevuları başarıyla silindi.";
            }
            return RedirectToAction("Hastalar");
        }
        [HttpPost]
        [HttpPost]
        public IActionResult DoktorSil(int id)
        {
            var randevular = _context.Randevular.Where(r => r.DoktorId == id).ToList();
            if (randevular.Any())
            {
                _context.Randevular.RemoveRange(randevular);
            }

            var doktor = _context.Doktorlar.FirstOrDefault(d => d.Id == id);
            if (doktor != null)
            {
                _context.Doktorlar.Remove(doktor);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Doktor ve ilişkili randevuları başarıyla silindi.";
            }
            return RedirectToAction("Doktorlar");
        }
        public IActionResult Ilaclar()
        {
            return View();
        }
        public IActionResult Randevular()
        {
            return View();
        }

    }
}
