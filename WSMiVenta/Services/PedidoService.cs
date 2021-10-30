using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace WSMiVenta.Services
{
    public class PedidoService : IPedidoService
    {

        //obtener todos los pedidos (con paginación) (admin)
        public List<PedidoRequest> getOrdersAdmin(int pag = 1)
        {
            var cantidadRegistrosPorPagina = 5; //numero de registros (ordenes) que se mostraran por página

            using (MiVentaContext db = new MiVentaContext())
            {
                PedidoRequest model = new PedidoRequest();
                List<PedidoRequest> lst = new List<PedidoRequest>(); //lista en la que se guardara todo                

                try
                {
                    foreach (var venta in db.Venta.ToList()) //recorremos la tabla Venta
                    {
                        var address = db.Direccions.Find(venta.IdDireccion);

                        LaDireccion direccion = new LaDireccion //lleno el modelo con la dirección encontrada
                        {
                            Id = address.Id,
                            Estado = address.Estado,
                            Colonia = address.Colonia,
                            Calle = address.Calle,
                            Numero = address.Numero
                        };

                        model = new PedidoRequest
                        {
                            IdUsuario = (int)venta.IdUsuario,
                            IdCliente = venta.IdCliente,
                            Fecha = venta.Fecha,
                            idVenta = venta.Id,
                            Total = venta.Total,
                            LaDireccion = direccion //se añade la dirección al modelo
                        };

                        //recorremos la tabla Coceptos, donde la id de la venta sea igual a la idVenta del concepto (para que vaya uniendo la venta con sus conceptos)
                        List<ElConcepto> lstConcept = new List<ElConcepto>(); //lista en donde se iran guardando los conceptos de cada venta
                        foreach (var conceptos in db.Conceptos.Where(i => i.IdVenta == venta.Id).ToList())
                        {                            
                            var prod = db.Productos.Find(conceptos.IdProducto); //busca en la tabla productos la idProducto que entra y le asigna el nombre del producto

                            ElConcepto concepto = new ElConcepto                            
                            {
                                IdVenta = venta.Id, //la id de la venta en coceptos debe ser la misma que la de mi id de mi tabla venta
                                NombreProducto = prod.Nombre,
                                IdProducto = conceptos.IdProducto,
                                Importe = conceptos.Importe,
                                PrecioUnitario = conceptos.PrecioUnitario,
                                Cantidad = conceptos.Cantidad,
                            };
                            lstConcept.Add(concepto); //se añade el objeto concepto a lstConcept (se hace así para evitar un error de incompatibilidad de datos)
                            
                        }
                        model.LosConceptos = lstConcept; // se le asigna lstConcept a mi modelo.LosConceptos
                        lst.Add(model); //se agregan a la lista                        
                    };

                }
                catch (Exception)
                {
                    throw new Exception("No se pudo mostrar los pedidos.");
                }
                //con skip le digo que se salte x pag, para no mostrar esas una vez pasamos de esa pagina ejemplo en la pag 2 no queremos ver los de la pag 1
                // es pag -1 porque si estamos en la 2 queremos que se salte los de la 1 porque esos ya los vimos
                // luego con take tomamos esos registros y los mostramos
                return lst.Skip((pag - 1) * cantidadRegistrosPorPagina) 
                    .Take(cantidadRegistrosPorPagina).ToList();
            }
        }









        //Obtener todos los pedidos (paginado) (usuario normal)
        public List<PedidoRequest> getOrdersUser(int idUsuario, int pag)
        {
            var cantidadDeRegistrosPorPagina = 5; //numero de pedidos a mostrar por página

            using (MiVentaContext db = new MiVentaContext())
            {                
                List<PedidoRequest> lst = new List<PedidoRequest>();
                PedidoRequest model = new PedidoRequest();

                try
                {
                    //recorremos la tabla ventas donde el id de usuario coincida con el recibido en el parametro
                    foreach (var venta in db.Venta.Where(i => i.IdUsuario == idUsuario).ToList())
                    {
                        var address = db.Direccions.Find(venta.IdDireccion); // buscamos la id dirección que coincida con la fk de id direccion de la venta

                        LaDireccion direccion = new LaDireccion
                        {                         
                            Id = address.Id,
                            Estado = address.Estado,
                            Colonia = address.Colonia,
                            Calle = address.Calle,
                            Numero = address.Numero
                        };

                        model = new PedidoRequest
                        {
                            idVenta = venta.Id,
                            IdUsuario = (int)venta.IdUsuario,
                            Fecha = venta.Fecha,
                            Total = venta.Total,
                            IdCliente = venta.IdCliente,
                            LaDireccion = direccion //se añade la dirección de esa venta al modelo
                        };

                        //recorremos la tabla conceptos en donde la idVenta sea igual a la id en tabla venta
                        List<ElConcepto> lstConcepto = new List<ElConcepto>(); // lista en donde se guardaran todos los conceptos de una venta, por cada repetición
                        foreach (var conceptos in db.Conceptos.Where(i => i.IdVenta == venta.Id).ToList())
                        {                            
                            var prod = db.Productos.Find(conceptos.IdProducto); //busca en la tabla productos la idProducto que entra y le asigna el nombre del producto

                            ElConcepto concepto = new ElConcepto
                            {
                                IdVenta = venta.Id,
                                NombreProducto = prod.Nombre,
                                Cantidad = conceptos.Cantidad,
                                Importe = conceptos.Importe,
                                PrecioUnitario = conceptos.PrecioUnitario,
                                IdProducto = conceptos.IdProducto,
                            };
                            
                            lstConcepto.Add(concepto); //agregamos a lstConcepto el objeto concepto por cada vez
                            model.LosConceptos = lstConcepto;// se le asignan los conceptos al modelo                            
                        }                        
                        lst.Add(model); //se añaden a la lista final
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    throw new Exception("No se pudo mostrar los pedidos del usuario.");
                }

                return lst.Skip((pag - 1) * cantidadDeRegistrosPorPagina)
                    .Take(cantidadDeRegistrosPorPagina).ToList();
            }
        }
    }
}
