using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VetClinic.Models;

namespace VetClinic.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string PasswordHash { get; set; }

        [Required]
        [Column("gender")]
        public string Gender { get; set; } = "Male";

        [Required]
        [Column("telephone")]
        public string Telephone { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("last_login")]
        public DateTime? LastLogin { get; set; } = DateTime.Now;

        public virtual Admin Admin { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Client Client { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}