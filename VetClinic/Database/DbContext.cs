using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Models;
using Microsoft.EntityFrameworkCore;
using VetClinic.MVVM.Model; // Assuming PrescriptionDrugs and AppointmentServices are here
using System.Diagnostics;

namespace VetClinic.Database
{
    // Files called clinic_data.sql and db_schema.sql are used to create the database and tables
    // Make sure, that the database is created before running the application
    public class VeterinaryClinicContext : DbContext
    {
        public VeterinaryClinicContext(DbContextOptions<VeterinaryClinicContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Opinion> Opinion { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<AppointmentServices> AppointmentServices { get; set; }
        public DbSet<PrescriptionDrugs> PrescriptionDrugs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- 1. Define Primary Keys for all entities first ---

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Doctor>()
               .HasIndex(d => d.Email)
               .IsUnique();

            // Pet (Assuming Pet has an 'Id' primary key)
            modelBuilder.Entity<Pet>()
                .HasKey(p => p.Id);

            // Appointment (Assuming Appointment has an 'Id' primary key)
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            // Prescription (Assuming Prescription has an 'Id' primary key, despite being one-to-one with Appointment)
            modelBuilder.Entity<Prescription>()
                .HasKey(p => p.Id);

            // Opinion (Assuming Opinion has an 'Id' primary key)
            modelBuilder.Entity<Opinion>()
                .HasKey(o => o.Id);

            // Drug (Assuming Drug has an 'Id' primary key)
            modelBuilder.Entity<Drug>()
                .HasKey(d => d.Id);

            // Service (Assuming Service has an 'Id' primary key)
            modelBuilder.Entity<Service>()
                .HasKey(s => s.Id);

            // Define Composite Primary Keys for junction tables.
            // This is crucial to ensure EF Core knows they have keys before relationships are mapped.
            modelBuilder.Entity<AppointmentServices>()
                .HasKey(asv => new { asv.AppointmentId, asv.ServiceId });

            modelBuilder.Entity<PrescriptionDrugs>()
                .HasKey(pd => new { pd.PrescriptionId, pd.DrugId });


            // --- 2. Define Relationships after all keys are established ---

            // Appointment relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            // One-to-one relationship between Appointment and Prescription
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Prescription)
                .WithOne(p => p.Appointment)
                .HasForeignKey<Prescription>(p => p.AppointmentId);

            // Many-to-many relationship via AppointmentServices
            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.AppointmentServices)
                .WithOne(asv => asv.Appointment)
                .HasForeignKey(asv => asv.AppointmentId);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.AppointmentServices)
                .WithOne(asv => asv.Service)
                .HasForeignKey(asv => asv.ServiceId);

            // Many-to-many relationship via PrescriptionDrugs
            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionDrugs)
                .WithOne(pd => pd.Prescription)
                .HasForeignKey(pd => pd.PrescriptionId);

            modelBuilder.Entity<Drug>()
                .HasMany(d => d.PrescriptionDrugs)
                .WithOne(pd => pd.Drug)
                .HasForeignKey(pd => pd.DrugId);

            // Opinion relationships
            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.User)
                .WithMany(c => c.Opinions)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Doctor)
                .WithMany(d => d.Opinions)
                .HasForeignKey(o => o.DoctorId);
        }
    }
}
