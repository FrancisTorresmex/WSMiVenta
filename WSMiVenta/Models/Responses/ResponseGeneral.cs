using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSMiVenta.Models.Responses
{
    public class ResponseGeneral
    {
        public int Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; } //un objeto se le puede agregar cualquier cosa

        public ResponseGeneral() //inicalizo el valor de success a 0, (0 = error, 1 = OK)
        {
            this.Success = 0; 
        }
    }
}
