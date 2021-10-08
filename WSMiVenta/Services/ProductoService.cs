using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class ProductoService : IProductoService
    {
        public void Add(ProductoRequest model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var product = new Producto();
                    product.Nombre = model.Nombre;
                    product.PrecioUnitario = model.PrecioUnitario;
                    product.Existencia = 1;
                    product.Url = model.Url;
                    db.Add(product);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Error, no se agrego el nuevo producto");
                }
            }
        }


        public void Edit(ProductoRequest model)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Producto product = db.Productos.Find(model.Id);
                    product.Nombre = model.Nombre;
                    product.PrecioUnitario = model.PrecioUnitario;
                    product.Existencia = model.Existencia;
                    product.Url = model.Url;
                    db.Update(product);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Error, no se puedo editar el producto");
                }
            }
        }

        //Eliminar
        public void Delete(int id)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Producto product = db.Productos.Find(id);
                    db.Remove(product);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Error al eliminar el producto");
                }
            }
        }        
    }
}
