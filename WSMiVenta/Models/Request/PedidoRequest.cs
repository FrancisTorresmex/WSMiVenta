using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class PedidoRequest
    {
        //[Required]
        //public Ventum LaVenta { get; set; }

        //[Required]
        //public Models.Concepto LosConceptos { get; set; }

        [Required]
        public long idVenta { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdUsuario { get; set; }        

        [Required]
        public DateTime Fecha { get; set; } 

        [Required]
        public decimal Total { get; set; }

        [Required]
        public List<ElConcepto> LosConceptos { get; set; }

    }

    public class ElConcepto
    {
        public int Cantidad { get; set; }

        public decimal Importe { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string NombrePorducto { get; set; }

        public int IdProducto { get; set; }

        public long IdVenta { get; set; }
    }
}
