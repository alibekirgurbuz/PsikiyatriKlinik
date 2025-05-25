using System;
using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public abstract class Kullanici
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Ad { get; set; }

        [Required, MaxLength(50)]
        public string Soyad { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(10)]
        public string Cinsiyet { get; set; }  // Erkek, Kadın, Diğer

        [Required, MaxLength(50)]
        public string Telefon { get; set; }

        [Required, MaxLength(100)]
        public string Sifre { get; set; }

        [Required]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        [Required]
        public string KullaniciTipi { get; set; }
    }
}
