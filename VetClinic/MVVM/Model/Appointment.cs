using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("appointment")]
    public class Appointment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("pet_id")]
        public int PetId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("is_completed")]
        public bool IsCompleted { get; set; }

        [Column("appointment_date")]
        public DateTime AppointmentDate { get; set; }

        [Column("reason_for_visit")]
        public string ReasonForVisit { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("diagnosis")]
        public string Diagnosis { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public Pet Pet { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<AppointmentServices> AppointmentServices { get; set; }
    }
}
