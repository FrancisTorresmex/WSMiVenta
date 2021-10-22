using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;

namespace WSMiVenta.Services
{
    public interface IUsuarioService
    {
        Usuario Registro(RegistroRequest model); //para registro

        AccessoResponse Autentificar(AccesoRequest model); //objeto de tipo AccessoResponse (para ingresar al sistema)

        public void EditUser(ModificarUsuarioRequest model); //Editar datos del usuario (nombre y contraseña)
    }
}
