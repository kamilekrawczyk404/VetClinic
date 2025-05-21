using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string telephone { get; set; }
        public DateTime date_of_birth { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? last_login { get; set; }

        public User user { get; set; }
        public Admin Admin { get; set; }
        public Doctor Doctor { get; set; }
        public Client Client { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
