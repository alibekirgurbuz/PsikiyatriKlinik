using Microsoft.AspNetCore.Mvc;
using PsikiyatriKlinik.Data;
using PsikiyatriKlinik.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace PsikiyatriKlinik.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Şifre eşleşme kontrolü
                if (model.Sifre != model.ConfirmPassword)
                {
                    TempData["ErrorMessage"] = "Şifreler uyuşmuyor.";
                    return View(model);
                }

                // Email kontrolü - Aynı email ile kayıtlı kullanıcı var mı?
                var existingUser = _context.Kullanicilar.FirstOrDefault(x => x.Email == model.Email);
                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Bu email adresi zaten kayıtlı.";
                    return View(model);
                }

                // Kullanıcı Tipine Göre Nesne Oluştur
                Kullanici yeniKullanici = model.KullaniciTipi switch
                {
                    "Hasta" => new Hasta
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Email = model.Email,
                        Sifre = model.Sifre,
                        Telefon = model.Telefon,
                        Cinsiyet = model.Cinsiyet,
                        KullaniciTipi = "Hasta"
                    },
                    "Doktor" => new Doktor
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Email = model.Email,
                        Sifre = model.Sifre,
                        Telefon = model.Telefon,
                        Cinsiyet = model.Cinsiyet,
                        KullaniciTipi = "Doktor"
                    },
                    "Admin" => new Admin
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Email = model.Email,
                        Sifre = model.Sifre,
                        Telefon = model.Telefon,
                        Cinsiyet = model.Cinsiyet,
                        YetkiSeviyesi = "Normal",
                        Notlar = "Yeni Admin kaydı",
                        KullaniciTipi = "Admin"
                    },
                    _ => null
                };

                if (yeniKullanici != null)
                {
                    _context.Kullanicilar.Add(yeniKullanici);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Kullanicilar
                            .FirstOrDefault(u => u.Email == model.Email && u.Sifre == model.Sifre);

                if (user != null)
                {
                    // Oturum açma işlemi
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserType", user.KullaniciTipi);
                    HttpContext.Session.SetString("UserName", user.Ad);

                    // Kullanıcı tipi kontrolü
                    switch (user.KullaniciTipi)
                    {
                        case "Hasta":
                            return RedirectToAction("HastaScreen", "Hasta");
                        case "Doktor":
                            return RedirectToAction("DoctorScreen", "Doktor");
                        case "Admin":
                            return RedirectToAction("AdminScreen", "Admin");
                    }
                }

                TempData["ErrorMessage"] = "Email veya şifre hatalı!";
            }

            return View(model);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            // Tüm oturum bilgilerini temizle
            HttpContext.Session.Clear();
            
            // Çerezleri temizle
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            
            // Oturum çerezini temizle
            Response.Cookies.Delete(".AspNetCore.Session");
            
            // Ana sayfaya yönlendir
            return RedirectToAction("Login");
        }

        // GET: Account/Profile
        public IActionResult Profile()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Login");
        }

        // POST: Account/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(Kullanici model)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.Email = model.Email;
                user.Telefon = model.Telefon;
                user.Cinsiyet = model.Cinsiyet;

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Profil bilgileri güncellendi.";
            }

            return RedirectToAction("Profile");
        }
    }
}
