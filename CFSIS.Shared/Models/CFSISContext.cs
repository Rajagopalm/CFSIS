using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CFSIS.Shared.Models
{
    public partial class CFSISContext : DbContext
    {
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<SubDistricts> SubDistricts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server= LENOVO-PC;Database=CFSIS;user id= sa;password=P@ssw0rd;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CourseTitle).HasMaxLength(256);
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.DistrictId);
            });

            modelBuilder.Entity<SubDistricts>(entity =>
            {
                entity.Property(e => e.SubDistrictsName).IsRequired();
            });
        }
    }
}
