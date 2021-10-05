using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IVentaService
    {
        public void Add(VentaRequest model);         
    }
}
