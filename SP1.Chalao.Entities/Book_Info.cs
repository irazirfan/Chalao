using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Entities
{
    [Table("Book_Info")]
    public partial class Book_Info
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int Bike_ID{ get; set; }

        [Required]
        public string Rider_Name { get; set; }

        [Required]
        [EmailAddress]
        public string Rider_Email { get; set; }

        public string Book_Schedule { get; set; }

        [ForeignKey("ID")]
        public Bike_Details BikeDetails { get; set; }

    }
}
