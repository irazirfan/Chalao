using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Entities
{
    [Table("Bike_Details")]
    public partial class Bike_Details
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Bike_ID { get; set; }

        [Required]
        public int Status { get; set; }

    }
}
