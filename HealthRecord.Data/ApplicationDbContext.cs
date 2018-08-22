using HealthRecord.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HealthRecord.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Creation> Creations { get; set; }

        public DbSet<Chip> Chips { get; set; }
        public DbSet<MedicalHistoryRecord> MedicalHistoryRecords { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
