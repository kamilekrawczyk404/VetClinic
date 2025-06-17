using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.MVVM.Model
{
    public class PrescriptionDrug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DosageForm { get; set; }
        public string Manufacturer { get; set; }
        public string Dosage { get; set; } = "";
        public int Quantity { get; set; } = 1;
    }
}
