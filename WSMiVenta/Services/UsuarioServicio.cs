using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Common;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;
using WSMiVenta.Tools;

namespace WSMiVenta.Services
{
    public class UsuarioServicio : IUsuarioService
    {
        private readonly AppSettingsCommon _appSettings;

        public UsuarioServicio(IOptions<AppSettingsCommon> appSettings)
        {
            this._appSettings = appSettings.Value; //queremos el valor que viene de IOptions de sistema
        }


        //Metodo de registro
        public Usuario Registro(RegistroRequest model)
        {
            using (MiVentaContext db = new MiVentaContext())
            {
                var user = new Usuario();                
                
                var search = db.Usuarios.Where(d => d.Email == model.Email).FirstOrDefault(); //busco si ya existe el correo

                if (search == null) //si no existe retornamos el usuario
                {
                    user.Email = model.Email;
                    user.Password = Encriptar.GetSHA256(model.Password); //encripto la contraseña a sha256
                    user.Nombre = model.Nombre;                    
                    user.IdRol = 6; //aqui debe ir el id del rol "normal"

                    db.Usuarios.Add(user);
                    db.SaveChanges();

                    return user;                                                     
                }
                return null; //si ya exisitia el correo retornamos null
            }
        }



        //Metodo de autentificar
        public AccessoResponse Autentificar(AccesoRequest model)
        {
            AccessoResponse access = new AccessoResponse();

            using (MiVentaContext db = new MiVentaContext())
            {                
                string spassword = Encriptar.GetSHA256(model.Password); //uso mi clase Encrypt con el metodo getSha256 y le paso de parametro de model recibido para encryptar

                // buscamos en la bd con la tabla Usuarios donde el Email recibido se encuentre, y tambien la contraseña encryptada sea igual 
                var user = db.Usuarios.Where(d => d.Email == model.email && d.Password == spassword).FirstOrDefault(); //el firstOrDefault regresa el primer elmento que coincida o nul                

                if (user == null) return null; // si no encuentra coincidencias retornamos null

                var NameRol = db.Rols.Find(user.IdRol); //buscamos en la tabla roles (esto para agregarle el nombre del rolal que pertenece el usuario al token)

                access.Id = user.Id;
                access.Email = user.Email; //si lo encuentra, asignamos
                access.Rol = NameRol.Nombre;
                //access.Rol = (int)user.IdRol;
                access.Token = GetToken(user);                                                          
                
            }

            return access;
        }


        //Método para generarle un token
        private string GetToken(Usuario usuario)
        {
            MiVentaContext db = new MiVentaContext();
            var NameRol = db.Rols.Find(usuario.IdRol); //buscamos en la tabla roles (esto para agregarle el nombre del rolal que pertenece el usuario al token)


            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity // los claims son los datos que queremos guardar
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()), //le agrego la id al token
                        new Claim(ClaimTypes.Email, usuario.Email), //le agrego el correo al token
                        new Claim(ClaimTypes.Role, NameRol.Nombre) //coloco validación del nombre de rol (esto sirve tambien para vlaidar el rol en el [Authorize = (Roles = "admin")])
                        //new Claim(ClaimTypes.Role,   usuario.Id.ToString()) //agrego el tipo de rol (esto sirve tambien para vlaidar el rol en el [Authorize(Roles = "5")])
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(15), //le agrego que expire cada 15 dias
                //encriptar la info
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature) //el HmacSha256Signature es el tipo de firma que tendra (elegi esa pero hay mas)                
            };

            //Creación del token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); //escribimos el token y lo retornamos
        }


        //Modificar datos de usuario
        public void EditUser (ModificarUsuarioRequest model)
        {
            using(MiVentaContext db = new MiVentaContext())
            {
                try
                {
                    Usuario user = db.Usuarios.Find(model.Id);                    

                    string spassword = Encriptar.GetSHA256(model.OldPassword); //encriptamos la contraseña para compararla con la que esta en la base
                    
                    if(user.Password == spassword) //si la contraseña coincide accedemos a cambiar los datos (nombre y contraseña)
                    {
                        string newPassword = Encriptar.GetSHA256(model.NewPassword); //encripto la nueva contraseña

                        user.Password = newPassword; 
                        user.Nombre = model.Nombre;
                        db.Update(user);
                        db.SaveChanges();
                    }                    

                }catch(Exception)
                {
                    throw new Exception("No se puedo editar el usuario correctamente");
                }
                
            }
        }
        
    }
}
