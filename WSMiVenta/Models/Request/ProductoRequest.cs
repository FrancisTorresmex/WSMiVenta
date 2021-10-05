using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class ProductoRequest
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="El idProducto debe ser mayor a 1")]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="El precioUnitario debe ser mayor a 0")]
        public decimal PrecioUnitario { get; set; }
    }
}
