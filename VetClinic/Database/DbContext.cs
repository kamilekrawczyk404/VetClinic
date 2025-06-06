using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Models;
using Microsoft.EntityFrameworkCore;
using VetClinic.MVVM.Model;

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
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Opinion> Opinion { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<AppointmentServices> AppointmentServices { get; set; }
        public DbSet<PrescriptionDrugs> PrescriptionDrugs { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User roles
            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Role - users
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relations between User and other entities
            // User - Client
            modelBuilder.Entity<User>()
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.Id);

            // User - Admin
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.Id);

            // User - Doctor
            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.Id);

            // Client
            modelBuilder.Entity<Client>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Pets)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            // Admin
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.Id);

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasKey(d => d.Id);

            // Appointment (has one pet and one doctore, many prescriptions and services)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AppointmentStatus)
                .WithMany(appStat => appStat.Appointments)
                .HasForeignKey(a => a.StatusId);

            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.Prescriptions)
                .WithOne(p => p.Appointment)
                .HasForeignKey(p => p.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.AppointmentServices)
                .WithOne(s => s.Appointment)
                .HasForeignKey(s => s.appointment_id);

            // Service
            modelBuilder.Entity<Service>()
                .HasMany(s => s.AppointmentServices)
                .WithOne(a => a.Service)
                .HasForeignKey(a => a.service_id);

            // Prescription
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentId);

            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionDrugs)
                .WithOne(d => d.Prescription)
                .HasForeignKey(d => d.prescription_id);

            // Opinion
            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Opinions)
                .HasForeignKey(o => o.ClientId);

            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Doctor)
                .WithMany(d => d.Opinions)
                .HasForeignKey(o => o.DoctorId);

            // Drug
            modelBuilder.Entity<Drug>()
                .HasMany(d => d.PrescriptionDrugs)
                .WithOne(p => p.Drug)
                .HasForeignKey(p => p.drug_id);
        }
    }
}
