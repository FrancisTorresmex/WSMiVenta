using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class EntregaService : IEntregaService
    {
        //Modificar estado de entrega 1(true) = entregado, 0(false) = pendiente (admin)
        public void deliveryStatus(EntregaRequest model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var search = db.Venta.Find( (long)model.IdVenta ); //Buscamos la id de venta

                    if(search == null)
                    {
                        throw new Exception();
                    }

                    search.Entrega = model.Entrega;
                    db.Venta.Update(search);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //throw new Exception("Error al modificar el estado de entrega");
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
