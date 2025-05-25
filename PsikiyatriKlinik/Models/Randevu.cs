using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsikiyatriKlinik.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HastaId { get; set; }

        [ForeignKey("HastaId")]
        public Hasta Hasta { get; set; }

        [Required]
        public int DoktorId { get; set; }

        [ForeignKey("DoktorId")]
        public Doktor Doktor { get; set; }

        [Required]
        public DateTime RandevuTarihi { get; set; }

        [Required]
        public TimeSpan RandevuSaati { get; set; }

        [Required, MaxLength(100)]
        public string DoktorAdi { get; set; }

        [MaxLength(50)]
        public string RandevuDurumu { get; set; } = "Beklemede";
    }
}
