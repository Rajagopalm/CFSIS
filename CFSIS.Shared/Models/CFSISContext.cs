﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CFSIS.Shared.Models
{
    public partial class CFSISContext : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<SubDistricts> SubDistricts { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderMasters> OrderMasters { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server= (local);Database=CFSIS;user id= sa;password=P@ssw0rd;Trusted_Connection=True;MultipleActiveResultSets=true");
        //            }
        //        }

        public CFSISContext(DbContextOptions<CFSISContext> options)
            : base(options)
        { }

        public CFSISContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

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

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailNo);

                entity.Property(e => e.OrderDetailNo).HasColumnName("Order_Detail_No");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("Item_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo).HasColumnName("Order_No");

                entity.Property(e => e.Qty).HasColumnName("QTY");
            });

            modelBuilder.Entity<OrderMasters>(entity =>
            {
                entity.HasKey(e => e.OrderNo);

                entity.Property(e => e.OrderNo).HasColumnName("Order_No");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("Order_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.TableId)
                    .IsRequired()
                    .HasColumnName("Table_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WaiterName)
                    .IsRequired()
                    .HasColumnName("Waiter_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

        }
    }
}
