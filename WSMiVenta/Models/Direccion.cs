using System;
using System.Collections.Generic;

#nullable disable

namespace WSMiVenta.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Venta = new HashSet<Ventum>();
        }

        public int Id { get; set; }
        public string Estado { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
