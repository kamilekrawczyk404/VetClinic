using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("drugs")]
    public class Drug
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("dosage_form")]
        public string DosageForm { get; set; }

        [Column("strength")]
        public string Strength { get; set; }

        [Column("unit_of_measure")]
        public string UnitOfMeasure { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("manufacturer")]
        public string Manufacturer { get; set; }

        [Column("stock_quantity")]
        public int Quantity { get; set; }

        public ICollection<PrescriptionDrugs> PrescriptionDrugs { get; set; }
    }
}
