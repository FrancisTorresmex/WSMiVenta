using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class PedidoRequest
    {
        public Ventum LaVenta { get; set; }

        public Models.Concepto LosConceptos { get; set; }

    }
}
