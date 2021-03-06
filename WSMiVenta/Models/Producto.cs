using System;
using System.Collections.Generic;

#nullable disable

namespace WSMiVenta.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Conceptos = new HashSet<Concepto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int? Existencia { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Concepto> Conceptos { get; set; }
    }
}
