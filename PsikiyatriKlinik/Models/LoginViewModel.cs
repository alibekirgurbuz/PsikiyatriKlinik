using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}
