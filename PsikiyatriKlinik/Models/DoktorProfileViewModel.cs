using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class DoktorProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [MaxLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Telefon en fazla 50 karakter olabilir.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
        [MaxLength(100, ErrorMessage = "Uzmanlık alanı en fazla 100 karakter olabilir.")]
        public string UzmanlikAlani { get; set; }

        [MaxLength(20, ErrorMessage = "Sicil no en fazla 20 karakter olabilir.")]
        public string SicilNo { get; set; }

        [MaxLength(100, ErrorMessage = "Klinik adı en fazla 100 karakter olabilir.")]
        public string KlinikAd { get; set; }

        [MaxLength(250, ErrorMessage = "Adres en fazla 250 karakter olabilir.")]
        public string Adres { get; set; }
    }
} 