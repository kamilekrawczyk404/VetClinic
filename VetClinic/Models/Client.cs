using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public User User { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
    }
}
