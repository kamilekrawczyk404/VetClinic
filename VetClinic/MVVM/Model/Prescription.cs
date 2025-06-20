﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("prescriptions")]
    public class Prescription
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("appointment_id")]
        public int AppointmentId { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        public Appointment Appointment { get; set; }
        public virtual ICollection<PrescriptionDrugs> PrescriptionDrugs { get; set; }

        [NotMapped]
        public int DrugsCount => PrescriptionDrugs?.Count ?? 0;
    }
}
