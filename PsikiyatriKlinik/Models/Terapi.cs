using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class Terapi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime TerapiTarihi { get; set; }

        [Required]
        public TimeSpan TerapiSuresi { get; set; }

        [MaxLength(500)]
        public string TerapistNotu { get; set; }

        [Required]
        public int DoktorId { get; set; }

        [ForeignKey("DoktorId")]
        public Doktor Doktor { get; set; }

        [Required]
        public int HastaId { get; set; }

        [ForeignKey("HastaId")]
        public Hasta Hasta { get; set; }
    }
}
