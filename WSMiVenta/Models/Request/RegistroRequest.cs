using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class RegistroRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Nombre { get; set; }
    }
}
