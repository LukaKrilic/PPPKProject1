using MedicalApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Data
{
    internal class MedicalDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Medication> Medications => Set<Medication>();
        public DbSet<SpecialistExam> SpecialistExams => Set<SpecialistExam>();
        public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var cs = Environment.GetEnvironmentVariable("MEDICAL_DB_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(cs)) cs = "Host=localhost;Username=user;Password=password;Database=medicaldb";
            options
                .UseNpgsql(cs)
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder mb) {
            mb.Entity<Patient>(e =>
            {
                e.Property(p => p.Oib).HasColumnType("char(11)");
                e.HasIndex(p => p.Oib).IsUnique();
                e.Property(p => p.Spol).HasColumnType("char(1)");
                e.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            });

            mb.Entity<SpecialistExam>(e =>
                e.Property(p => p.TipPregleda).HasConversion<string>().HasMaxLength(10));

            mb.Entity<Prescription>(e =>
                e.Property(x => x.Doza).HasPrecision(10, 2));
        }
    }
}
