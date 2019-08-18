using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppCoreEF2.Models
{
    public partial class SchoolExtendedContext : DbContext
    {
        public SchoolExtendedContext()
        {
        }

        public SchoolExtendedContext(DbContextOptions<SchoolExtendedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonExtended> PersonExtended { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-391R7HE2;Database=SchoolExtended;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<PersonExtended>(entity =>
            {
                entity.HasKey(e => e.Personid);

                entity.Property(e => e.Personid).ValueGeneratedNever();
            });
        }
    }
}
