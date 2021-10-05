using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class VentaRequest
    {
        public int Id { get; set; }

        //public DateTime Fecha { get; set; } //esto se obtendra en el controller
        //public double total { get; set; }

        public List<Concepto> Conceptos { get; set; }

    }

    public class Concepto //modelo de concepto (estas dos tablas venta y concepto van unidas)
    {
        [Range(1, double.MaxValue, ErrorMessage ="Cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        public decimal Importe { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int IdProducto { get; set; }

    }
}
