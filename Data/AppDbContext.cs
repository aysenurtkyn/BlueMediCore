using BlueMediCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueMediCore.Data
{
    // DbContext sınıfından miras alıyoruz (Entity Framework'ün ana sınıfı)
    public class AppDbContext : DbContext
    {
        // Constructor: Ayarları (Connection String) Program.cs'den alıp buraya taşır.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Entities klasöründeki sınıfları veritabanı tablolarına dönüştürür
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // CreatedAt & UpdatedAt otomatik yönetimi
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
