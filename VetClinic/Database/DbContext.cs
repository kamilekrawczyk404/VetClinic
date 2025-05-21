using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;

namespace VetClinic.Database
{
    public class VeterinaryClinicContext : DbContext
    {
        public VeterinaryClinicContext(DbContextOptions<VeterinaryClinicContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User roles
            modelBuilder.Entity<Role>()
                .HasOne(r => r.User)
                .WithMany(u => u.Roles);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne(r => r.User);

            // Admin
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.id);

            // Client
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.id);

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.id);

            // Pet
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.client_id);

            // Appointment (has one pet and one doctore, many prescriptions and services)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.pet_id);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.doctor_id);

            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.Prescriptions)
                .WithOne(p => p.Appointment)
                .HasForeignKey(p => p.appointment_id);

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
                .HasForeignKey(p => p.appointment_id);

            modelBuilder.Entity<Prescription>()
                .HasMany(p => p.PrescriptionDrugs)
                .WithOne(d => d.Prescription)
                .HasForeignKey(d => d.prescription_id);

            // Opinion
            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Opinions)
                .HasForeignKey(o => o.client_id);

            modelBuilder.Entity<Opinion>()
                .HasOne(o => o.Doctor)
                .WithMany(d => d.Opinions)
                .HasForeignKey(o => o.doctor_id);

            // Drug
            modelBuilder.Entity<Drug>()
                .HasMany(d => d.PrescriptionDrugs)
                .WithOne(p => p.Drug)
                .HasForeignKey(p => p.drug_id);
        }
    }
}
