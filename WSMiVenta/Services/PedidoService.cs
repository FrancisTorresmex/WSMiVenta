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
        //Carga los modelos con la información necesaria a mostrar
        public List<PedidoRequest> loadOrders(List<PedidoRequest> lst)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                PedidoRequest model = new PedidoRequest();
                lst = new List<PedidoRequest>(); //lista en la que se guardara todo                

                try
                {
                    foreach (var venta in db.Venta.OrderByDescending(i => i.Id).ToList()) //recorremos la tabla Venta
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
                            Entrega = venta.Entrega,
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
                    return lst;
                }
                catch (Exception)
                {
                    throw new Exception("Error al cargar los pedidos.");
                }
            }
        }



        //obtener todos los pedidos (con paginación) (admin)
        public List<PedidoRequest> getOrdersAdmin(int pag = 1)
        {
            var cantidadRegistrosPorPagina = 10; //numero de registros (ordenes) que se mostraran por página

            List<PedidoRequest> lst = new List<PedidoRequest>();
            try
            {
                lst = loadOrders(lst);
            }
            catch (Exception)
            {
                throw new Exception("Error al mostrar los pedidos");
            }

            //con skip le digo que se salte x pag, para no mostrar esas una vez pasamos de esa pagina ejemplo en la pag 2 no queremos ver los de la pag 1
            // es pag -1 porque si estamos en la 2 queremos que se salte los de la 1 porque esos ya los vimos
            // luego con take tomamos esos registros y los mostramos
            return lst.Skip((pag - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();
        }



        //Buscar un pedido (admin)
        public List<PedidoRequest> searchOrderAdmin(int id)
        {
            List<PedidoRequest> lst = new List<PedidoRequest>();

            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var search = db.Venta.Find((long)id);

                    if (search == null)
                    {
                        throw new Exception();
                    }
                    //Asignamos a lst lo recibido del metodo loadOrders pero solo donde la idVenta de la tabla Venta conicida con la id
                    lst = loadOrders(lst).Where(i => i.idVenta == id).ToList();
                }
                catch (Exception)
                {
                    throw new Exception("Error al buscar pedido");
                }
                return lst;
            }
        }


        //Buscar en historial de pedidos los que tengan estado pendiente (admin)
        public List<PedidoRequest> searchPendingAdmin(bool delivery, int pag = 1)
        {
            var cantidadRegistrosPorPagina = 10; //numero de registros (ordenes) que se mostraran por página
            List<PedidoRequest> lst = new List<PedidoRequest>();

            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    //Buscamos en la tabla venta donde es estado de entrega coincida con lo que tenga delivery
                    //true = entregado, false = pendiente
                    lst = loadOrders(lst).Where(i => i.Entrega == delivery).ToList(); 

                }
                catch (Exception)
                {
                    throw new Exception("Error al buscar en historial");
                }

                //con skip le digo que se salte x pag, para no mostrar esas una vez pasamos de esa pagina ejemplo en la pag 2 no queremos ver los de la pag 1
                // es pag -1 porque si estamos en la 2 queremos que se salte los de la 1 porque esos ya los vimos
                // luego con take tomamos esos registros y los mostramos
                return lst.Skip((pag - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();                
            }
        }





        //Obtener todos los pedidos (paginado) (usuario normal)
        public List<PedidoRequest> getOrdersUser(int idUsuario, int pag = 1)
        {
            var cantidadDeRegistrosPorPagina = 5; //numero de pedidos a mostrar por página

            List<PedidoRequest> lst = new List<PedidoRequest>();

            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var serach = db.Usuarios.Find(idUsuario); //buscamos en la tabla usuario la Id recibida

                    if(serach == null) //si es null o no es enocntrado mandamos al catch
                    {
                        throw new Exception();
                    }
                    //Si no es null, entonces ejecutamos el metodo y solo asigamos a lst en donde la Id de usario en la tabla Venta coincida con la Id de la tabla usuario
                    lst = loadOrders(lst).Where(i => i.IdUsuario == idUsuario).ToList();
                }
                catch (Exception)
                {
                    throw new Exception("Error al cargar el historial de pedidos");
                }

                return lst.Skip((pag - 1) * cantidadDeRegistrosPorPagina)
                        .Take(cantidadDeRegistrosPorPagina).ToList();
            }            
        }



        //Buscar un pedido (usuario normal)
        public List<PedidoRequest> searchOrderUser(int idUsuario, int idVenta)
        {
            List<PedidoRequest> lst = new List<PedidoRequest>();

            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var search = db.Venta.Find((long)idVenta);
                    var user = db.Usuarios.Find(idUsuario);

                    if(search == null || user == null)
                    {
                        throw new Exception();
                    }
                    //agregamos a la lista lo del metodo solo dondé la id del usuario de la tabla venta coincida con la dada.
                    // y donde la id de la venta coinicda con la id de venta dada
                    //en pocas palabras el usuario solo puede buscar entre sus pedidos y no obtener información que no le pertence
                    lst = loadOrders(lst).Where(i => i.IdUsuario == user.Id && i.idVenta == search.Id).ToList();                                        
                }
                catch (Exception)
                {
                    throw new Exception("Error al buscar el pedido");
                }
                return lst;
            }
        }


        //Buscar en historial de pedidos los que tengan estado pendiente (usario normal)
        public List<PedidoRequest> searchPendingUser(int idUser, bool delivery, int pag = 1)
        {
            var cantidadRegistrosPorPagina = 5; //numero de pedidos a mostrar por página
            List<PedidoRequest> lst = new List<PedidoRequest>();

            using (MiVentaContext db = new MiVentaContext())
            {                
                try
                {
                    var user = db.Usuarios.Find(idUser);

                    if (user == null)
                    {
                        throw new Exception();
                    }

                    //Buscamos en la tabla venta donde es estado de entrega coincida con lo que tenga delivery
                    //true = entregado, false = pendiente
                    lst = loadOrders(lst).Where(i => i.Entrega == delivery && i.IdUsuario == user.Id).ToList();

                }
                catch (Exception)
                {
                    throw new Exception("Error al buscar en historial de usuario");
                }

                //con skip le digo que se salte x pag, para no mostrar esas una vez pasamos de esa pagina ejemplo en la pag 2 no queremos ver los de la pag 1
                // es pag -1 porque si estamos en la 2 queremos que se salte los de la 1 porque esos ya los vimos
                // luego con take tomamos esos registros y los mostramos
                return lst.Skip((pag - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
            }
        }

    }
}
