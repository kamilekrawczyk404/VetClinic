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
    [Table("prescriptiondrugs")]
    public class PrescriptionDrugs
    {
        [Column("prescription_id")]
        [Key]
        public int PrescriptionId { get; set; }

        [Column("drug_id")]
        [Key]
        public int DrugId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("dosage")]
        public string Dosage { get; set; }

        public virtual Drug Drug { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
