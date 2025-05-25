using System;
using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50)]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [MaxLength(50)]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [MaxLength(11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır.")]
        [MinLength(11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır.")]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [MaxLength(50)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Adres { get; set; }

        public DateTime? DogumTarihi { get; set; }
    }
}
