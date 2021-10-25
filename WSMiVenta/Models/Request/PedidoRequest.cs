using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class PedidoRequest
    {
        [Required]
        public Ventum LaVenta { get; set; }

        [Required]
        public Models.Concepto LosConceptos { get; set; }
    }
}
