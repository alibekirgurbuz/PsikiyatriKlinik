using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class Ilac
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string IlacAdi { get; set; }

        [Required, MaxLength(50)]
        public string IlacTuru { get; set; } // Ör: Tablet, Şurup, Kapsül

        [Required]
        public int StokDurumu { get; set; }

        [Required]
        public string Doz { get; set; } // Ör: 500mg, 10ml

        [Required, MaxLength(50)]
        public string KullanimSikligi { get; set; } // Ör: Günde 2 kez, 3 saat arayla
    }
}
