using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class VentaRequest
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public int IdDireccion { get; set; }

        //public DateTime Fecha { get; set; } //esto se obtendra en el controller
        //public double total { get; set; }

        [Required]
        public bool Entrega { get; set; }

        [Required]
        public Direccion Direccion { get; set; }

        [Required]
        public List<Concepto> Conceptos { get; set; }

    }

    public class Direccion
    {
        public int Id { get; set; }

        public string Estado { get; set; }

        public string Colonia { get; set; }

        public string Calle { get; set; }

        public int Numero { get; set; }
    }


    public class Concepto //modelo de concepto (estas tablas venta y concepto van unidas)
    {
        [Range(1, double.MaxValue, ErrorMessage ="Cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        public decimal Importe { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public int IdProducto { get; set; }

    }
}
