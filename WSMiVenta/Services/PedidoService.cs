﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class PedidoService : IPedidoService
    {

        //obtener todos los pedidos (con paginación) (admin)
        public List<Ventum> getOrdersAdmin(int pag = 1)
        {
            var cantidadRegistrosPorPagina = 5; //numero de registros (ordenes) que se mostraran por página

            using (MiVentaContext db = new MiVentaContext())
            {
                PedidoRequest model = new PedidoRequest();
                List<Ventum> lst = new List<Ventum>(); //lista en la que se guardara todo

                try
                {
                    foreach (var venta in db.Venta.ToList()) //recorremos la tabla Venta
                    {
                        model.LaVenta = new Ventum
                        {
                            Id = venta.Id,
                            IdUsuario = venta.IdUsuario,
                            Total = venta.Total
                        };

                        //recorremos la tabla Coceptos, donde la id de la venta sea igual a la idVenta del concepto (para que vaya uniendo la venta con sus conceptos)
                        foreach (var conceptos in db.Conceptos.Where(i => i.IdVenta == venta.Id).ToList())
                        {
                            model.LosConceptos = new Models.Concepto // los conceptos de mi modelo seran un nuevo objeto de mi tabla conceptos
                            {
                                IdVenta = venta.Id, //la id de la venta en coceptos debe ser la misma que la de mi id de mi tabla venta
                                Id = conceptos.Id,
                                IdProducto = conceptos.IdProducto,
                                Importe = conceptos.Importe,
                                PrecioUnitario = conceptos.PrecioUnitario,
                                Cantidad = conceptos.Cantidad,
                            };
                            model.LaVenta.Conceptos.Add(model.LosConceptos);   //asigno los conceptos                         
                        }
                        lst.Add(model.LaVenta); //se agregan a la lista                        
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
        public List<Ventum> getOrdersUser(int idUsuario, int pag = 1)
        {
            var cantidadDeRegistrosPorPagina = 5; //numero de pedidos a mostrar por página

            using (MiVentaContext db = new MiVentaContext())
            {
                PedidoRequest model = new PedidoRequest();
                List<Ventum> lst = new List<Ventum>();

                try
                {
                    //recorremos la tabla ventas donde el id de usuario coincida con el recibido en el parametro
                    foreach (var venta in db.Venta.Where(i => i.IdUsuario == idUsuario).ToList())
                    {
                        model.LaVenta = new Ventum
                        {
                            Id = venta.Id,
                            Fecha = venta.Fecha,
                            IdUsuario = venta.IdUsuario,
                            Total = venta.Total
                        };                        

                        //recorremos la tabla conceptos en donde la idVenta sea igual a la id en tabla venta
                        foreach (var conceptos in db.Conceptos.Where(i=> i.IdVenta == venta.Id))
                        {
                            model.LosConceptos = new Models.Concepto
                            {
                                Cantidad = conceptos.Cantidad,
                                IdProducto = conceptos.IdProducto,
                                Importe = conceptos.Importe,
                                PrecioUnitario = conceptos.PrecioUnitario,
                                Id = conceptos.Id,
                                IdVenta = conceptos.IdVenta
                            };
                            model.LaVenta.Conceptos.Add(model.LosConceptos); //se agregan los conceptos al modelo
                        }
                        lst.Add(model.LaVenta);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("No se pudo mostrar los pedidos del usuario.");
                }
                return lst.Skip((pag - 1) * cantidadDeRegistrosPorPagina)
                    .Take(cantidadDeRegistrosPorPagina).ToList();
            }
        }
    }
}
