using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP1.Chalao.Entities;

namespace SP1.Chalao.Model
{
    [Table("Users")]
    public class RegistrationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password & Confirm Password don't match")]
        [Display(Name = "Confirm Password")]
        public string CPassword { get; set; }

        [Required]
        public string Mobile { get; set; }
        [Display(Name = "User Type")]
        public int User_TypeID { get; set; }

    }
}
