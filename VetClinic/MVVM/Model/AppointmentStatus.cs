using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.MVVM.Model
{
    [Table("appointmentstatus")]   
    public class AppointmentStatus
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
