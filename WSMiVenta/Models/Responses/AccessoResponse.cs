using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Responses
{
    public class AccessoResponse
    {
        public int Id { get; set; } //id del usuario

        public string Email { get; set; }       
        
        public int Rol { get; set; }

        public string Token { get; set; }
    }
}
