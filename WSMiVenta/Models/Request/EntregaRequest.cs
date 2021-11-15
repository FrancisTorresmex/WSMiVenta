using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Request
{
    public class EntregaRequest
    {
        [Required]
        public int IdVenta { get; set; }

        [Required]
        public bool Entrega { get; set; }

        ////constructor
        //public EntregaRequest()
        //{
        //    Entrega = false; //Entrega por defecto sera false al inicio (se modificara según se requiera)
        //}
    }
}
