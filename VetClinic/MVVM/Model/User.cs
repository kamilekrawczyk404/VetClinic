using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetClinic.Models;

namespace VetClinic.Models
{
    [Table("users")]
    public class User
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

        [Column("password")]
        public string PasswordHash { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("phone_number")]
        public string TelephoneNumber { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("last_login")]
        public DateTime? LastLogin { get; set; } = DateTime.Now;

        public virtual ICollection<Pet> Pets { get; set; }

        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}