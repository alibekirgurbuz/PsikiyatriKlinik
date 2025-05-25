using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class Doktor : Kullanici
    {
        [Required, MaxLength(100)]
        public string UzmanlikAlani { get; set; }

        [MaxLength(250)]
        public string Adres { get; set; }

        public DateTime BaslamaTarihi { get; set; }

        [MaxLength(20)]
        public string SicilNo { get; set; }

        [MaxLength(100)]
        public string KlinikAd { get; set; }
        public ICollection<Randevu> Randevular { get; set; }  // Navigation Property
    }
}
