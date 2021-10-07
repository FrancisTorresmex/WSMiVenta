using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;

namespace WSMiVenta.Services
{
    public class RolService : IRolService
    {
        public Rol Add(RolRequest model)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    var rol = new Rol();
                    Rol search = db.Rols.Where(d => d.Nombre == model.Nombre).FirstOrDefault();
                    if (search != null) return null;

                    rol.Nombre = model.Nombre;
                    db.Add(rol);
                    db.SaveChanges();
                    return rol;
                }
                catch (Exception)
                {
                    throw new Exception("Error al crear el rol");
                }
            }
        }
        
        public void Edit(RolRequest model)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                try
                {                    
                    Rol rol = db.Rols.Find(model.Id); //buscamos en la tabla rol el id
                    rol.Nombre = model.Nombre;
                    db.Update(rol);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Error al actualizar el rol");                    
                }
            }
        }
    }
}
