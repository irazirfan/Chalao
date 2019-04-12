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
    [Table("Admins")]
    public partial class Admins
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        [ForeignKey("ID")]
        public virtual Users Users { get; set; }

    }
}
