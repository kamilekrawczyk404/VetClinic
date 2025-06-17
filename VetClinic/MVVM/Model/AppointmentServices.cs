using Microsoft.EntityFrameworkCore;
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
        [Column("appointment_id")]
        [Key]
        public int AppointmentId { get; set; }

        [Column("service_id")]
        [Key]
        public int ServiceId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Service Service { get; set; }
    }
}
