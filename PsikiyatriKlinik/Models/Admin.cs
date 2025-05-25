using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class Admin : Kullanici
    {
        [MaxLength(100)]
        public string YetkiSeviyesi { get; set; }

        [MaxLength(250)]
        public string Notlar { get; set; }

        public Admin()
        {
            KullaniciTipi = "Admin";
        }
    }
}
