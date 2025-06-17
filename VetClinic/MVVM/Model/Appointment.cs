using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.MVVM.Model;

namespace VetClinic.Models
{
    [Table("appointments")]
    public class Appointment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("pet_id")]
        public int PetId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("reason_for_visit")]
        public string ReasonForVisit { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("diagnosis")]
        public string? Diagnosis { get; set; }

        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public Pet Pet { get; set; }
        public Doctor Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual ICollection<AppointmentServices> AppointmentServices { get; set; }
    }
}
