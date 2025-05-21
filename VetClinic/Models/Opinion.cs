using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Opinion
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int doctor_id { get; set; }
        public string comment { get; set; }
        public int rating { get; set; }
        public DateTime created_at { get; set; }

        public ICollection<Opinion> Opinions { get; set; }
        public Client Client { get; set; }
        public Doctor Doctor { get; set; }
    }
}
