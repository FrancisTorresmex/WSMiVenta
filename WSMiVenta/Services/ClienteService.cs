using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class ClienteService : IClienteService
    {        

        //Agregar clientes
        public void Add(ClienteRequest model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var client = new Cliente();
                    client.Nombre = model.Nombre;
                    db.Add(client);
                    db.SaveChanges(); //al momento de guardar, entity framework le asignara una id al cliente (es autoincrementable)
                }
                catch (Exception)
                {
                    throw new Exception("Error, no se agrego el cliente");
                }
            }
        }


        public void Edit(ClienteRequest model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Cliente client = db.Clientes.Find(model.Id);   //buscamos                 
                    client.Nombre = model.Nombre; //asignamos nuevo valor
                    db.Update(client); //hacemos update
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("No se pudo editar el cliente correctamente");
                }
            }
        }


        public void Delete(int idCliente)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Cliente client = db.Clientes.Find(idCliente);
                    db.Remove(client);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Error al eliminar el cliente");
                }
            }
        }
        
    }
}
