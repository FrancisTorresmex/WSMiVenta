using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using WSMiVenta.Tools;

namespace WSMiVenta.Services
{
    public class UsuarioService : IUsuarioService
    {

        //Editar nombre de usuario
        public void EditName(UpdateName model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Usuario user = db.Usuarios.Find(model.Id);//Buscamos el usuario

                    if(user == null) //si no es encontrado o es null, mandamos excepción para que termine en el catch
                    {
                        throw new Exception();
                    }

                    user.Nombre = model.Nombre; //se cambia el nombre
                    db.Usuarios.Update(user);
                    db.SaveChanges();

                }catch(Exception)
                {
                    throw new Exception("Error al módificar el nombre de usuario");
                }
            }
        }


        //Cambiar nombre de usuario
        public void EditPassword(UpdatePassword model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Usuario user = db.Usuarios.Find(model.Id);

                    if(user == null) 
                    {
                        throw new Exception();
                    }

                    //Encripto la contraseña recibida del modelo para compararla con la de la base
                    string sPassword = Encriptar.GetSHA256(model.OldPassword);

                    if(user.Password == sPassword) //Si la contraseña del modelo y la de la base coinciden
                    {
                        string newPassword = Encriptar.GetSHA256(model.NewPassword); //Encriptamos la nueva contraseña
                        user.Password = newPassword; //Asigmaos la nueva contraseña
                        db.Usuarios.Update(user);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
                catch (Exception)
                {
                    throw new Exception("Error al módificar la contraseña");
                }
            }
        }
    }
}
