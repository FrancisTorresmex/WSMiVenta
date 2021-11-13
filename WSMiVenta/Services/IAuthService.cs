using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;

namespace WSMiVenta.Services
{
    public interface IAuthService
    {
        Usuario Registro(RegistroRequest model); //para registro

        AccessoResponse Autentificar(AccesoRequest model); //objeto de tipo AccessoResponse (para ingresar al sistema)        
    }
}
