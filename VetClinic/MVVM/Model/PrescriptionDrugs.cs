using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("prescriptiondrugs")]
    public class PrescriptionDrugs
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("prescription_id")]
        public int PrescriptionId { get; set; }

        [Column("drug_id")]
        public int DrugId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("dosage_instructions")]
        public string DosageInstructions { get; set; }

        public virtual Drug Drug { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
