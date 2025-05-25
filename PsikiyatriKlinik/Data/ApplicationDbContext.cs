using Microsoft.EntityFrameworkCore;
using PsikiyatriKlinik.Models;

namespace PsikiyatriKlinik.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Hasta> Hastalar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Terapi> Terapiler { get; set; }
        public DbSet<Ilac> Ilaclar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH - Table Per Hierarchy - Tüm Kullanıcılar tek tabloda tutulacak
            modelBuilder.Entity<Kullanici>()
                        .HasDiscriminator<string>("KullaniciTipi")
                        .HasValue<Hasta>("Hasta")
                        .HasValue<Doktor>("Doktor")
                        .HasValue<Admin>("Admin");

            modelBuilder.Entity<Randevu>()
                        .HasOne(r => r.Hasta)
                        .WithMany(h => h.Randevular)
                        .HasForeignKey(r => r.HastaId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Randevu>()
                        .HasOne(r => r.Doktor)
                        .WithMany(d => d.Randevular)
                        .HasForeignKey(r => r.DoktorId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Terapi>()
                        .HasOne(t => t.Hasta)
                        .WithMany()
                        .HasForeignKey(t => t.HastaId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Terapi>()
                        .HasOne(t => t.Doktor)
                        .WithMany()
                        .HasForeignKey(t => t.DoktorId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
