using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("client")]
    public class Client
    {
        // this field in related to the table called User, it's id column is a foreign key to the table User, and it also is a primary key, make good relation

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
    }
}
