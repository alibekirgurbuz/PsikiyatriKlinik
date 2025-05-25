using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PsikiyatriKlinik.Models
{
    public class RandevuViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        public DateTime RandevuTarihi { get; set; }

        [Required(ErrorMessage = "Randevu saati zorunludur.")]
        public TimeSpan RandevuSaati { get; set; }

        [Required(ErrorMessage = "Doktor seçimi zorunludur.")]
        public int DoktorId { get; set; }

        public string? DoktorAdi { get; set; }

        public string RandevuDurumu { get; set; } = "Beklemede";
    }
}
