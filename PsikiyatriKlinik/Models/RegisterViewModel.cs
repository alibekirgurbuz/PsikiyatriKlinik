using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(50)]
        public string Ad { get; set; }

        [Required, MaxLength(50)]
        public string Soyad { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Telefon { get; set; }

        [Required, MaxLength(10)]
        public string Cinsiyet { get; set; }

        [Required, MaxLength(100)]
        public string Sifre { get; set; }

        [Required, MaxLength(100)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string KullaniciTipi { get; set; }  // Hasta, Doktor, Admin
    }
}
