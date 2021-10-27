using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class VentaService : IVentaService
    {
        public void Add(VentaRequest model)
        {
            using (MiVentaContext db = new MiVentaContext())
            {                
                using (var transaction = db.Database.BeginTransaction()) //con BeginTransaction iniciamos una inserccion en la base (que se podra revertir si sale algo mal)
                {
                    try
                    {

                        var address = new Models.Direccion(); // objeto de la tabla Dirección

                        address.Estado = model.Direccion.Estado; //llenado del modelo de tipo dirección
                        address.Colonia = model.Direccion.Colonia;
                        address.Calle = model.Direccion.Calle;
                        address.Numero = model.Direccion.Numero;
                        db.Direccions.Add(address);
                        db.SaveChanges();

                        var sale = new Ventum(); //Ventum es mi tabla venta, pero se cambio el nombre por comando

                        sale.Total = model.Conceptos.Sum(op => op.Cantidad * op.PrecioUnitario); //operación para obtener el total
                        sale.Fecha = DateTime.Now;
                        sale.IdUsuario = model.IdUsuario; //id del usuario que realiza la compra 
                        sale.IdCliente = model.IdCliente;
                        sale.IdDireccion = address.Id;
                        db.Venta.Add(sale);
                        db.SaveChanges(); //al momento de guardar, entity framework le asignara una id a venta                        

                        foreach (var item in model.Conceptos)
                        {
                            var concept = new Models.Concepto(); //tipo Models.Concepto (es el modelo de concepto dentro de VentaRequest)

                            concept.Cantidad = item.Cantidad;
                            concept.Importe = item.Importe;
                            concept.PrecioUnitario = item.PrecioUnitario;
                            concept.IdProducto = item.IdProducto;
                            concept.IdVenta = sale.Id; //para este punto sale.id ya tiene asignada una, asi que sera la misma que en concepto

                            db.Conceptos.Add(concept);
                            db.SaveChanges();
                        }
                        transaction.Commit(); // sin esto, las tablas se bloquearian al pasar por aqui y quedarian bloqueadas(es impirtante ponerlo)
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); //si la transaccion falla, con esto regreso al estado anterior antes así es como si esa transacción
                        throw new Exception("Error al agregar la compra");
                        //Console.WriteLine(ex.InnerException);
                    }
                }                
            }
        }
    }
}
