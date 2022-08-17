using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KTX.Models
{
    public class Admin
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Email Address is required")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Required]
        public string? ComfirmNewPassword { get; set; }

    }

}
