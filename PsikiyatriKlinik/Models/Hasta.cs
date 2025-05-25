using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class Hasta : Kullanici
    {
        [MaxLength(250)]
        public string Adres { get; set; }

        public DateTime? DogumTarihi { get; set; }

        [MaxLength(20)]
        public string KanGrubu { get; set; }

        [Required, MaxLength(11)]
        public string TcKimlikNo { get; set; }

        [MaxLength(100)]
        public string AcilDurumKisi { get; set; }

        [MaxLength(20)]
        public string AcilDurumTelefon { get; set; }
        public ICollection<Randevu> Randevular { get; set; }  // Navigation Property
    }
}
