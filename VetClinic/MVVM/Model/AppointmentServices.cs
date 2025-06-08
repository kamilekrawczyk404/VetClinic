using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("appointmentservices")]
    public class AppointmentServices
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        public Appointment Appointment { get; set; }
        public Service Service { get; set; }
    }
}
