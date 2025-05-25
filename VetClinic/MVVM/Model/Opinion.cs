using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    [Table("opinion")]
    public class Opinion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("commnet")]
        public string Comment { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("created_at")]  
        public DateTime CreatedAt { get; set; }

        public Client Client { get; set; }
        public Doctor Doctor { get; set; }
    }
}
