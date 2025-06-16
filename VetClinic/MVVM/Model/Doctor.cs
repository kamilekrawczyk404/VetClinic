using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Models
{
    [Table("doctors")]
    public class Doctor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("password")]
        public string PasswordHash { get; set; }

        [Column("phone_number")]
        public string TelephoneNumber { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("last_login")]
        public DateTime? LastLogin { get; set; } = DateTime.Now;

        [NotMapped]
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}
