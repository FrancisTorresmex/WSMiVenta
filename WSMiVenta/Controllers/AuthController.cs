using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSMiVenta.Models.Request;
using WSMiVenta.Models.Responses;
using WSMiVenta.Services;

namespace WSMiVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _user;

        public AuthController(IAuthService user)
        {
            this._user = user;
        }

        //Ingresar
        [HttpPost("Login")]
        public IActionResult Autentificar([FromBody] AccesoRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();

            var user = _user.Autentificar(model); //recordemos que el metodo Autoentificar puede retornar null o los datos del usuario segun se encuentre en la bd o no

            if (user == null)
            {
                response.Message = "Usuario o contraseña incorrectos";
                //return BadRequest(response);
                return Ok(response);
            }

            //si existe
            response.Success = 1;
            response.Data = user;

            return Ok(response);
        }


        //Registro
        [HttpPost("Registro")]
        public IActionResult Registro([FromBody] RegistroRequest model)
        {
            ResponseGeneral response = new ResponseGeneral();
            var user = _user.Registro(model);

            if (user == null) //si el correo ya existe
            {
                response.Message = "El Correo ya existe";
                //return BadRequest(response);
                return Ok(response);
            }

            //si no existe
            response.Success = 1;
            //response.Data = user;

            return Ok(response);
        }        
    }
}
