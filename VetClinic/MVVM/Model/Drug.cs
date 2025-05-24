using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Drug
    {
        public int id { get; set; }
        public string name { get; set; }
        public string dosage_form { get; set; }
        public string strength { get; set; }
        public string unit_of_measurement { get; set; }
        public string description { get; set; }
        public string manufacturer { get; set; }
        public int quantity { get; set; }

        public ICollection<Drug> Drugs { get; set; }
        public ICollection<PrescriptionDrugs> PrescriptionDrugs { get; set; }
    }
}
