using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{    
    public class UpdateName
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }

    public class UpdatePassword
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
