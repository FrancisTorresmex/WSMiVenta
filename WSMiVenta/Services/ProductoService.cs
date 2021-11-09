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

        public List<Producto> Get(int pag = 1) //pag por defecto sera 1, por si no se inidca que pagina
        {
            var cantidadDeregistrosPorPagina = 3; //numero de productos a mostrar por página                            

            using (MiVentaContext db = new MiVentaContext())
            {
                var lst = db.Productos.OrderBy(i => i.Id).ToList();
                //Retornamos la lista con pag -1 le decimos que si esta en la página 2, muestre los de la pag 2 skipeando los de la pag 1, asi solo veremos los de la 2
                //el take es para tomar esos registros (productos) y mostrar solo lo que se nos pide
                return lst
                        .Skip((pag - 1) * cantidadDeregistrosPorPagina)
                        .Take(cantidadDeregistrosPorPagina).ToList();
            }
        }                       
    


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

        //Buscar
        public List<Producto> Search(int Id, char nombre)
        {
            List<Producto> lst = new List<Producto>();

            using (MiVentaContext db = new MiVentaContext())
            {                
                try
                {
                    foreach (var productos in db.Productos.ToList())
                    {                        
                        //si en la busqueda la id coincide con la id de el producto buscado ó las letras coinciden con parte del nombre de un producto
                        //se agrega a la lista
                        if (productos.Id.Equals(Id) || productos.Nombre.Contains(nombre))
                        {
                            lst.Add(productos);                            
                        }                      
                    }
                    return lst;
                }
                catch(Exception)
                {
                    throw new Exception("Error al buscar el producto");
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
