using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Models
{
    [Table("doctor")]
    public class Doctor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [NotMapped]
        public bool HasClientOpinion { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}
