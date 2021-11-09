using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public interface IProductoService
    {
        List<Producto> Get(int pag);

        public void Add(ProductoRequest model);

        public void Edit(ProductoRequest model);

        List<Producto> Search(int id, char nombre);

        public void Delete(int id);
    }
}
