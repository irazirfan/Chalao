using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Entities
{
    [Table("Riders")]
    public partial class Riders
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Gender_ID { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [ForeignKey("ID")]
        public virtual Users Users { get; set; }

    }
}
