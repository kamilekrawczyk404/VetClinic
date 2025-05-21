using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class PrescriptionDrugs
    {
        public int id { get; set; }
        public int prescription_id { get; set; }
        public int drug_id { get; set; }
        public int quantity { get; set; }
        public string dosage_instructions { get; set; }

        public Drug Drug { get; set; }
        public Prescription Prescription { get; set; }
    }
}
